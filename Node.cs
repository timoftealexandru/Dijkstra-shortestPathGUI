using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApmDijkstra
{
    class Node
    {
         private string _name;

        // constructor
        public Node(string name)
        {
            this._name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
