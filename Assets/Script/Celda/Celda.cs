using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celda
{
    public int xGrid, yGrid;

    public bool transitable;

    public Celda padre;

    public int costeG;
    public int costeH;
    public int costeF 
    { 
        get {
            return costeG + costeH;
        }
    }

    public Celda(int xGrid = 0, int yGrid = 0, bool transitable = true, int costeG = 0, int costeH = 0)
    {
        this.xGrid = xGrid;
        this.yGrid = yGrid;
        this.transitable = transitable;
        this.padre = padre;
        this.costeG = costeG;
        this.costeH = costeH;
    }
}
