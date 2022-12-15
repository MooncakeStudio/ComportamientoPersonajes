using System.Collections.Generic;
using System.Linq;

//using System.Runtime.Remoting.Messaging; este using da error

using Assets.Scripts.DataStructures;

using UnityEngine;

namespace Assets.Scripts.SampleMind
{
    public class AEstrella : AbstractPathMind {


        // declarar Stack de Locomotion.MoveDirection de los movimientos hasta llegar al objetivo
        private Stack<Locomotion.MoveDirection> currentPlan = new Stack<Locomotion.MoveDirection>();

        public override void Repath()
        {
            // limpiamos la pila currentPlan
            currentPlan.Clear();
        }

        public override Locomotion.MoveDirection GetNextMove(BoardInfo board, CellInfo currentPos, CellInfo[] goals)
        {
            
            // si la Stack no está vacía, hacer siguiente movimiento
             if (currentPlan.Count != 0)
             {
                 // devuelve siguiente movimiento (pop the Stack)
                
                currentPlan.Pop();
             }
            
            // calcular camino, devuelve resultado de A*
            var searchResult = Search(board, currentPos, goals);

            // recorre searchResult and copia el camino a currentPlan
            while (searchResult.getPadre() != null)
            {
                currentPlan.Push(searchResult.getMovimiento());
                searchResult = searchResult.getPadre();
            }

            // returns next move (pop Stack)
             if (currentPlan.Any()){
                 return currentPlan.Pop();
             }

            return Locomotion.MoveDirection.None;

        }

        private Nodo Search(BoardInfo board, CellInfo start, CellInfo[] goals)
        {
            // crea una lista vacía de nodos
            var open = new List<Nodo> ();
            // crea una lista auxiliar para luego ordenar la lista open
            var aux = new List<Nodo> ();

            // nodo inicial
            var n = new Nodo(start);
            // se hace set de sus atributos
            n.sethEstrella(hEstrella(n,goals));
            n.setfEstrella(n.gethEstrella());

            // añade nodo inicial a la lista
            open.Add(n);

            // creamos una variable para controlar que no haga demasiadas búsquedas
            int salirBucle = 0;


            // mientras la lista no esté vacia
            while (open.Any())
            {
                // mira el primer nodo de la lista
                var nodoPrimero = open.First();
                open.RemoveAt(0);

                // si el primer nodo es goal, returns current node
                if (nodoPrimero.gethEstrella()==0)//el goal
                {
                    Nodo meta = nodoPrimero;
                    return meta;
                }

                // expande vecinos (calcula coste de cada uno, etc)y los añade en la lista
                var vecinos = nodoPrimero.getCelda().WalkableNeighbours(board);               
                
                for(int i=0; i<4;i++){
                    if(vecinos[i]!= null){
                        var nuevoVecino = new Nodo(vecinos[i]);

                        // comprobamos la dirección que tiene que almacenar el nodo                       
                        if(i==0){
                                nuevoVecino.setMovimiento(Locomotion.MoveDirection.Up); // la posicion 0 del array es Up
                                }
                        else if(i==1){
                                nuevoVecino.setMovimiento(Locomotion.MoveDirection.Right); // la posicion 1 del array es Right
                                }
                        else if(i==2){
                                nuevoVecino.setMovimiento(Locomotion.MoveDirection.Down); // la posicion 2 del array es Down
                                }
                        else if(i==3){
                                nuevoVecino.setMovimiento(Locomotion.MoveDirection.Left); // la posicion 3 del array es Left
                                }        

                        // se hace set de los atributos del nuevo nodo    
                        nuevoVecino.sethEstrella(hEstrella(nuevoVecino, goals));
                        nuevoVecino.setPadre(nodoPrimero);                                  
                        nuevoVecino.setG((nuevoVecino.getPadre().getG()) + nuevoVecino.getCelda().WalkCost);
                        nuevoVecino.setfEstrella(nuevoVecino.getG()+nuevoVecino.gethEstrella());     

                        // se añade el nodo a la lista
                        open.Add(nuevoVecino);
                    }                   
                }

                // ordena lista
                aux=open.OrderBy(ex=>ex.getfEstrella()).ToList();
                open=aux;

                // comprobamos el número de búsquedas
                if (salirBucle == 2500)
                {
                    Debug.Log("No se ha podido encontrar una solución");
                    return nodoPrimero;
                }

                salirBucle++;
            }
            return null;
        } 
        
        private int hEstrella(Nodo ex, CellInfo[] goals){       

                // estrella da el valor del h* del nodo ex respecto al goal posicionado en el índice 0 del array goals
                // este número se calcula haciendo la distancia manhattan       
                int estrella=(System.Math.Abs(goals[0].ColumnId - ex.getCelda().ColumnId))+(System.Math.Abs(goals[0].RowId - ex.getCelda().RowId));

                // devolvemos el valor de h*
                return estrella;
        }
    }           
}

           












//Clase de tipo nodo (padre, hijo, operación asociada, coste)
//clase de tipo árbol de búsqueda??? (lista de tipo nodo)

//Primera vez que ejecutemos la función deberá hacer un plan de búsqueda A*, devolver 1er movimiento
//Siguientes veces ejecuta el plan de búsqueda de la primera ejecución, no lo vuelve a crear
//Cellinfo tiene función getNeighbours (hijos de una celda)

//Va a cambiar la semilla del Loader a la hora de corregir

//A* para ir a la meta (entorno estático) -- Distancia Manhattan
//Ascenso de colinas para matar a los enemigos (entorno dinámico) -- Búsqueda`por horizonte


