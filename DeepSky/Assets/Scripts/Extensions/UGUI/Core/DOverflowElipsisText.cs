using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DOverflowElipsisText : Text {
    	   
    public void SetText(string content)
    {
        if (horizontalOverflow == HorizontalWrapMode.Overflow)
        {
            text = content;
            return;
        }
        font.RequestCharactersInTexture(content, fontSize, fontStyle);
        CharacterInfo characterInfo;
        float width = 0;
        float elipsisWidth = Utilities.GameUtils.GetElipsisTextHorIsOverflow(font, fontSize);
        for (int i = 0; i < content.Length; i++)
        {
            char tmpChar = content[i];
            font.GetCharacterInfo(tmpChar, out characterInfo, fontSize);
            width = width + characterInfo.advance;
            if ((width + elipsisWidth) > this.rectTransform().sizeDelta.x)
            {
                text = content.Substring(0, Mathf.Max(0, i-1)) + "...";
                return;
            }
        }
        text = content;
    }
}
