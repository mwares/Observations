using Parse;
using System;
using System.Collections.Generic;
using System.Text;


namespace Observations.Entities
{
    public class Pupil
    {
        public string Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Image { get; set; }
    }

    //public class Item : System.ComponentModel.INotifyPropertyChanged
    //{
    //    public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

    //    protected virtual void OnPropertyChanged(string propertyName)
    //    {
    //        if (this.PropertyChanged != null)
    //        {
    //            this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    //        }
    //    }

    //    private string _Title = string.Empty;
    //    public string Title
    //    {
    //        get
    //        {
    //            return this._Title;
    //        }

    //        set
    //        {
    //            if (this._Title != value)
    //            {
    //                this._Title = value;
    //                this.OnPropertyChanged("Title");
    //            }
    //        }
    //    }

    //    private string _Forename = string.Empty;
    //    public string Forename
    //    {
    //        get
    //        {
    //            return this._Forename;
    //        }

    //        set
    //        {
    //            if (this._Forename != value)
    //            {
    //                this._Forename = value;
    //                this.OnPropertyChanged("Forename");
    //            }
    //        }
    //    }

    //    private object _Image = null;
    //    public object Image
    //    {
    //        get
    //        {
    //            return this._Image;
    //        }

    //        set
    //        {
    //            if (this._Image != value)
    //            {
    //                this._Image = value;
    //                this.OnPropertyChanged("Image");
    //            }
    //        }
    //    }

    //    public void SetImage(Uri baseUri, String path)
    //    {
    //        //Image = new BitmapImage(new Uri(baseUri, path));
    //        throw new NotImplementedException();
    //    }

    //    private string _Link = string.Empty;
    //    public string Link
    //    {
    //        get
    //        {
    //            return this._Link;
    //        }

    //        set
    //        {
    //            if (this._Link != value)
    //            {
    //                this._Link = value;
    //                this.OnPropertyChanged("Link");
    //            }
    //        }
    //    }

    //    private string _Category = string.Empty;
    //    public string Category
    //    {
    //        get
    //        {
    //            return this._Category;
    //        }

    //        set
    //        {
    //            if (this._Category != value)
    //            {
    //                this._Category = value;
    //                this.OnPropertyChanged("Category");
    //            }
    //        }
    //    }

    //    private string _Description = string.Empty;
    //    public string Description
    //    {
    //        get
    //        {
    //            return this._Description;
    //        }

    //        set
    //        {
    //            if (this._Description != value)
    //            {
    //                this._Description = value;
    //                this.OnPropertyChanged("Description");
    //            }
    //        }
    //    }

    //    private string _Content = string.Empty;
    //    public string Content
    //    {
    //        get
    //        {
    //            return this._Content;
    //        }

    //        set
    //        {
    //            if (this._Content != value)
    //            {
    //                this._Content = value;
    //                this.OnPropertyChanged("Content");
    //            }
    //        }
    //    }
    //}

    public class PupilSurname
    {
        public string Surname { get; set; }

        public List<Pupil> Pupils { get; set; }
    }

    public class GroupInfoList<T> : List<object>
    {
        public object Key { get; set; }

        public new IEnumerator<object> GetEnumerator()
        {
            return (System.Collections.Generic.IEnumerator<object>)base.GetEnumerator();
        }
    }
}
