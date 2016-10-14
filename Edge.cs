using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApmDijkstra
{
    class Edge
    {
        private Node _origin;
        private Node _destination;
        private double _distance;
 
        //constructor pentru muchii
        public Edge(Node origin, Node destination, double distance)
        {
            this._origin = origin;
            this._destination = destination;
            this._distance = distance;
        }

        public Node Origin
        {
            get
            {
                return _origin;
            }
        }

        public Node Destination
        {
            get
            {
                return _destination;
            }
        }

        public double Distance
        {
            get
            {
                return _distance;
            }
        }
       
    }
}
