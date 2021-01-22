using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SweetSugar.Scripts.Items
{
    public class MarmaladeFly : UnityEngine.MonoBehaviour
    {
        public GameObject explosionPrefab;
        public GameObject particles;
        private Action callback;
        private bool reachedTarget;
        private int priority = 1;
        private bool canBeStarted;
        public ItemsTypes nextItemType;
        public bool setJelly;
        public Vector2Int[] targets;
        public float animationTime = 1.5f;
        private Vector3 pos, scale;
        private Quaternion rot;
        public int originSortingOrder = 2;
        public Vector3 startDirection;
        private ItemMarmalade thisItem;
        public Item TargetItem;

        public void StartFly()
        {
            particles.SetActive(true);
            GetComponent<SpriteRenderer>().sortingLayerName = "ItemMask";
            GetComponent<SpriteRenderer>().sortingOrder = 10;
            canBeStarted = true;
            foreach (var target in targets)
            {
                var item = LevelManager.THIS.field.GetSquare(target.x, target.y).Item;
                if (item.marmaladeTarget == null && TargetItem == null)
                {
                    TargetItem = item;
                    TargetItem.marmaladeTarget = gameObject;
//                SetAnimationKeys();
                }
            }
        }

        private void Awake()
        {
            thisItem = GetComponentInParent<ItemMarmalade>();
            pos = transform.localPosition;
            rot = transform.localRotation;
            scale = transform.localScale;
        }

        private void OnEnable()
        {
            particles.SetActive(false);
            TargetItem = null;
            transform.localPosition = pos;
            transform.localRotation = rot;
            transform.localScale = scale;
        }

        private void OnDisable()
        {
            reachedTarget = false;
            LeanTween.cancel(gameObject);
            if (TargetItem != null) TargetItem.marmaladeTarget = null;
            TargetItem = null;
        
            StopAllCoroutines();
            GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            GetComponent<SpriteRenderer>().sortingOrder = originSortingOrder;
            transform.localPosition = pos;
            transform.localRotation = rot;
            transform.localScale = scale;
        }


        private void ReachItem()
        {
            if (TargetItem != null && TargetItem.gameObject.activeSelf)
            {
                if (setJelly)
                    TargetItem.square.SetType(SquareTypes.JellyBlock, 1, SquareTypes.NONE, 1);
                reachedTarget = true;
                callback?.Invoke();
                if (nextItemType == ItemsTypes.NONE ||
                    (TargetItem.currentType != ItemsTypes.NONE && TargetItem.currentType != ItemsTypes.MULTICOLOR))
                {
                    TargetItem.DestroyItem(true, true, thisItem);
                }
                else
                {
                    TargetItem.NextType = nextItemType;
                    TargetItem.ChangeType();
                    TargetItem.DestroyItem();
                }

                // targetItem.square.DestroyBlock();
            }

            DestroyMarmalade();
        }

        private void DestroyMarmalade()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            LevelManager.THIS.FindMatches();
            gameObject.SetActive(false);
        }

        internal void SetDirection(Vector2 v)
        {
            Random.InitState(GetHashCode());
            var seq = LeanTween.sequence();
            seq.append(LeanTween.move(gameObject, transform.position - startDirection, animationTime ));
            seq.append(LeanTween.move(gameObject, LevelManager.THIS.field.GetPosition() + Random.insideUnitCircle*3 , animationTime).setOnComplete(() =>
            {
                if(gameObject.activeSelf)
                    StartCoroutine(FindTargetLoop());}));
            LeanTween.scale(gameObject, Vector3.one * 1.5f, animationTime );
            LeanTween.rotateAround(gameObject, Vector3.forward, 360, animationTime);
        }

        public void SecondPartDestroyAnimation(Action _callback)
        {
            callback = _callback;
            //MoveBack();
        }

        IEnumerator FindTargetLoop()
        {
            while (TargetItem == null)
            {
                FindTarget();
                yield return new WaitForSeconds(.01f);
            }
            LeanTween.cancel(gameObject);
            LeanTween.move(gameObject, TargetItem.transform, 0.4f).setEase(LeanTweenType.easeInOutBack).setOnUpdate(CheckTargetUpdate).setOnComplete(ReachItem);
            LeanTween.scale(gameObject, Vector2.one*1.2f , 0.4f);
        }

        void CheckTargetUpdate(float f)
        {
            if (TargetItem.destroying || TargetItem == null || !TargetItem.gameObject.activeSelf)
            {
                StopCoroutine(FindTargetLoop());
                LeanTween.pause(gameObject);
                TargetItem = null;
                StartCoroutine(FindTargetLoop());
            }
        }

        private void FindTarget()
        {
            //Find hard to reach alone items or cages
            var items = LevelManager.THIS.field.GetLonelyItemsOrCage()
                .Where(i => ArgItems(i));
            //If the marmalade should spread a jelly looking for non jellied squares
            if (setJelly)
                items = LevelManager.THIS.field.squaresArray.Where(i => i.type != SquareTypes.NONE && i.type != SquareTypes.JellyBlock).Select(i => i.Item).Where(i =>
                    i != null && i
                        .Explodable).MarmaladeCondition(gameObject, thisItem);
            //trying to find ingredients
            if (!items.Any())
            {
                var list = LevelManager.THIS.field.squaresArray.Where(i => i.type != SquareTypes.NONE).Select(i => i.Item).Where(i =>
                    i != null && i.GetComponent<TargetComponent>() && !i
                        .Combinable);
                if (list.Any())
                    items = list.Where(i=>i.square.nextSquare != null && i.square.nextSquare.Item && i.square.nextSquare.Item.Explodable).Select(i => i.square.nextSquare?.Item).MarmaladeCondition(gameObject, thisItem);
            }

            //If still not found items:
            if (!items.Any())
            {
                //looking for another item which is target of the current level 
                if (LevelManager.THIS.levelData.target.prefabs.FirstOrDefault()?.GetType().BaseType == typeof(Square) ||
                    LevelManager.THIS.levelData.target.prefabs.FirstOrDefault()?.GetType() == typeof(LayeredBlock))
                    items = LevelManager.THIS.field.squaresArray.Where(i => i.type == (SquareTypes) Enum.Parse(typeof(SquareTypes), LevelManager.THIS.levelData.target.name))
                        .Select(i => i.Item).Where(i => ArgItems(i)).MarmaladeCondition(gameObject, thisItem);
            }

            //Looking through all items
            if (items == null || !items.Any())
            {
                items = LevelManager.THIS.field.GetItems().Where(i => i.Explodable).MarmaladeCondition(gameObject, thisItem);
            }

            TargetItem = items.MarmaladeCondition(gameObject, thisItem).OrderBy(i => Vector3.Distance(transform.position, i.transform.position))
                .FirstOrDefault();
            if (TargetItem != null && TargetItem != thisItem && (TargetItem.marmaladeTarget == null || TargetItem.marmaladeTarget == gameObject))
            {
                TargetItem.marmaladeTarget = gameObject;
            }
        }

        private static bool ArgItems(Item i)
        {
            return i.Explodable && !i.needFall && !i.falling && !i.destroying && !i.JustCreatedItem;
        }

        public bool IsAnimationFinished()
        {
            return reachedTarget;
        }

        public int GetPriority()
        {
            return priority;
        }

        public bool CanBeStarted()
        {
            return canBeStarted;
        }
    }
}

public static class MarmaladeUtils
{
    public static IEnumerable<Item> MarmaladeCondition(this IEnumerable<Item> seq, GameObject gameObject, Item item)
    {
        return seq.WhereNotNull().Where(i => (i.marmaladeTarget == null || i.marmaladeTarget == gameObject)&&i != item && !i.destroying);
    }  
}