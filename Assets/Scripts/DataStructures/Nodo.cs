using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.DataStructures{
    public class Nodo
    {

        private Nodo padre; // nodo padre
        private CellInfo miCelda; // info de la celda
        private int hEstrella; // distancia manhattan
        private float g; // camino recorrido
        private Locomotion.MoveDirection movimiento; // direccion de movimiento
        private float fEstrella; // (h* + g)

        //  CONSTRUCTOR
        public Nodo(CellInfo infoCelda){
                this.miCelda = infoCelda;
            }

        // GETTERS
        public Locomotion.MoveDirection getMovimiento(){
            return this.movimiento;
        }

        public CellInfo getCelda(){
            return this.miCelda;
        }

        public float getG(){
            return this.g;
        }

        public Nodo getPadre(){
            return padre;
        }

        public int gethEstrella(){
            return hEstrella;
        }

        public float getfEstrella(){
            return fEstrella;
        }

        //SETTERS
        public void setMovimiento(Locomotion.MoveDirection x){
            this.movimiento = x;
        }

        public void setPadre(Nodo padre){
            this.padre = padre;
        }

        public void setG(float nuevaG){
            this.g = nuevaG;
        }

        public void sethEstrella(int h){
            this.hEstrella=h;
        }

        public void setfEstrella(float f){
            this.fEstrella=f;
        }
    }
}
