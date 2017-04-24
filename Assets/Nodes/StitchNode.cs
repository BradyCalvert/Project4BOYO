using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StitchNode : NodeBaseClass {




    public StitchNode(Rect r, int ID) : base(r,ID)
    {

    }

    public  void DrawGUI(int winID)
    { 
               BaseDraw(winID);
    }

    public  void AttachComplete(int winID)
    {
        base.linkedNodes.Add(winID);
    }
}
