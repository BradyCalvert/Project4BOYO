using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodesEditor : EditorWindow
{
    public List<NodeBaseClass> myWindows = new List<NodeBaseClass>();
    public Spool mySpool;
    [MenuItem("Window/Node editor")]


    static void ShowWindow()
    {
        NodesEditor editor = EditorWindow.GetWindow<NodesEditor>();
    }
    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        mySpool = (Spool)EditorGUILayout.ObjectField(mySpool, typeof(Spool), false);
        if(EditorGUI.EndChangeCheck())
        {
            myWindows.Clear();
            if(mySpool!=null)
            {
                for (int i = 0; i < mySpool.stitchCollection.Length; i++)
                {
                    myWindows.Add(new StitchNode(new Rect(100 * i, 20, 100, 100), i));
                }
            }
        }
        if (mySpool != null)
        {



            for (int i = 0; i < mySpool.stitchCollection.Length; i++)
            {
                for (int j = 0; j < mySpool.stitchCollection[i].yarns.Length; j++)
                {
                    if (myWindows[mySpool.stitchCollection[i].yarns[j].choiceStitch.stitchID].rect != null)
                    {
                        DrawNoxeCurve(myWindows[i].rect, myWindows[mySpool.stitchCollection[i].yarns[j].choiceStitch.stitchID].rect);
                    }
                }
            }
        }
        mySpool =(Spool) EditorGUILayout.ObjectField(mySpool, typeof(Spool), false);
        BeginWindows();
        for (int i = 0; i < myWindows.Count; i++)
        {
            myWindows[i].rect = GUI.Window(i, myWindows[i].rect, myWindows[i].DrawGUI,mySpool.stitchCollection[i].stitchName);
        }

        EndWindows();
    }

    void DrawNoxeCurve(Rect start, Rect end)
    {
        Vector3 starPos = new Vector3(start.x + start.width, start.y + (start.height / 2), 0);
        Vector3 endPos = new Vector3(end.x, end.y + (end.height / 2), 0);
        Vector3 startTan = starPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Handles.DrawBezier(starPos, endPos, startTan, endTan, Color.black, null, 1);
    }

    public void RemoveNode(int winID)
    {
        RemoveAttatchments(winID);
        myWindows.RemoveAt(winID);

        ReassignIDs();
    }

    public void ReassignIDs()
    {
        for (int i = 0; i < myWindows.Count; i++)
        {
            myWindows[i].id -= 1;
        }
    }
    public void RemoveAttatchments(int winID)
    {
        for (int i = 0; i < myWindows.Count; i++)
        {
            myWindows[i].linkedNodes.Remove(winID);
        }
    }
}