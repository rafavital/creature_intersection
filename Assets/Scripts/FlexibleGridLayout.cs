using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType{Uniform, Width, Height, FixedRows, FixedColumns}

    [Space]
    public FitType fitType;
    
    [Space]
    public int rows;
    public int columns;
    
    [Space]
    public Vector2 cellSize;
    public Vector2 spacing;
    
    [Space]
    public bool fitX;
    public bool fitY;
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        padding.top = (int) Mathf.Clamp (padding.top, 0, Mathf.Infinity);

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;

            float sqrRt = Mathf.Sqrt (transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float) columns);
        }
        if (fitType == FitType.Height || fitType == FitType.FixedRows) 
        {
            columns = Mathf.CeilToInt (transform.childCount / (float) rows);
        }

        rows = (int) Mathf.Clamp (rows, 1, Mathf.Infinity);
        columns = (int) Mathf.Clamp (columns, 1, Mathf.Infinity);

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float) columns - ((spacing.x/(float)columns) * (columns-1)) - (padding.left / (float) columns) - (padding.right / (float) columns);
        float cellHeight = parentHeight / (float) rows - ((spacing.y/(float)rows) * (columns-1)) - (padding.top / (float) rows) - (padding.bottom / (float) rows);
        
        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];
            
           
           
            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;
            
            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);

        }
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool IsActive()
    {
        return base.IsActive();
    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {
    }

    public override string ToString()
    {
        return base.ToString();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnBeforeTransformParentChanged()
    {
        base.OnBeforeTransformParentChanged();
    }

    protected override void OnCanvasGroupChanged()
    {
        base.OnCanvasGroupChanged();
    }

    protected override void OnCanvasHierarchyChanged()
    {
        base.OnCanvasHierarchyChanged();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void OnDidApplyAnimationProperties()
    {
        base.OnDidApplyAnimationProperties();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
    }

    protected override void OnTransformChildrenChanged()
    {
        base.OnTransformChildrenChanged();
    }

    protected override void OnTransformParentChanged()
    {
        base.OnTransformParentChanged();
    }

    protected override void OnValidate()
    {
        base.OnValidate();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void Start()
    {
        base.Start();
    }
}
