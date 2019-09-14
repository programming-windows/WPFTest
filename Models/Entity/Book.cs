using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entity
{
    public class Book : BasicEntity ,INotifyPropertyChanged
    {
        /*
        public string id { get; set; }
        public int year { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public string authorFirst { get; set; }
        public string authorLast { get; set; }
        public string publisher { get; set; }
        */



        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                Changed("id");
            }
        }


        private int year;
        public int Year{
            get
            {
                return year;
            }
            set {
                year = value;
                Changed("year");
            }
        }

        private string title;
        public string Title{
            get
            {
                return title;
            }
            set {
                title = value;
                Changed("title");
            }
        }
        private double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                Changed("price");
            }
        }
        private string authorFirst;
        public string AuthorFirst
        {
            get
            {
                return authorFirst;
            }
            set
            {
                authorFirst = value;
                Changed("authorFirst");
            }
        }
        private string authorLast;
        public string AuthorLast
        {
            get
            {
                return authorLast;
            }
            set
            {
                authorLast = value;
                Changed("authorLast");
            }
        }

        private string publisher;
        public string Publisher
        {
            get
            {
                return publisher;
            }
            set
            {
                publisher = value;
                Changed("publisher");
            }
        }
        
        
        #region 属性更改通知
        public event PropertyChangedEventHandler PropertyChanged;
        private void Changed(string PropertyName)
        {
            entityState = EntityState.MODIFIED;
            
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
        

    }
}
