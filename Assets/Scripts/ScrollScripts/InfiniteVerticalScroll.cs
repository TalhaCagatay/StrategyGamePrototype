﻿using UnityEngine;
using UnityEngine.UI;

public class InfiniteVerticalScroll : InfiniteScroll {

    protected override float GetSize(RectTransform item)
    {
        return item.GetComponent<LayoutElement>().minHeight + content.GetComponent<VerticalLayoutGroup>().spacing;
    }

    protected override float GetDimension(Vector2 vector)
    {
        return vector.y;
    }

    protected override Vector2 GetVector(float value)
    {
        return new Vector2(0, value);
    }

    protected override float GetPos(RectTransform item)
    {
        return item.localPosition.y + content.localPosition.y;
    }

    protected override int OneOrMinusOne()
    {
        return 1;
    }
}
