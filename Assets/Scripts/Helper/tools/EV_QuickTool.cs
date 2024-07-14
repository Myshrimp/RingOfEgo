using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EV
{
    namespace Tool
    {
        public class EV_QuickTool : MonoBehaviour
        {
            //
            public static bool getStringCompareEasy(string input, string target)
            {
                return (!target.Equals("") && !target.Equals(" ") && (target.Contains(input) || target.Equals(input)));
            }
            // Start is called before the first frame update
            void Start()
            {

            }

            // Update is called once per frame
            void Update()
            {

            }
        }
    }
}
