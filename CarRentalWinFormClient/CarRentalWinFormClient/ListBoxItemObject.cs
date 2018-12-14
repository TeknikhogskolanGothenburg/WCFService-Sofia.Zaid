using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalWinFormClient
{
    public class ListBoxItemObject
    {
       // public List<String> SelectedItemAttributes { get; set; }
        string Attribute { get; set; }
        public int SelectedItemId { get; set; }

        //public ListBoxItemObject(List<String> attributes, int id)
        //{
        //    foreach(string s in attributes)
        //    {
        //        this.SelectedItemAttributes.Add(s);
        //    }
        //    this.SelectedItemId = id;
       // }
        public ListBoxItemObject(string attributes, int id)
        {
            this.Attribute = attributes;
            this.SelectedItemId = id;
        }

        public ListBoxItemObject(string attributes)
        {
            this.Attribute = attributes;
        }

        public override string ToString()
        {
            //string attributes = "";
            //foreach(string s in SelectedItemAttributes)
            //{
            //    attributes = attributes + " " + s;
            //}
            return this.Attribute + " " + SelectedItemId.ToString();
        }
    }
}
