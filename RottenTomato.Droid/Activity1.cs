using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using RottenTomato.Core;

namespace RottenTomato.Droid
{
    [Activity(Label = "RottenTomato.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private List<string> _titles = new List<string>();
        private ListView _list;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            _list = FindViewById<ListView>(Resource.Id.listView1);

            button.Click += async delegate
                {
                    var root = await RottenTomatoApi.GetMovieList("battlestar");
                    ShowTitles(root);
                };
        }

        private void ShowTitles(RootObject root)
        {
            _titles.Clear();
            foreach (var movie in root.movies)
            {
                _titles.Add(movie.title);
            }

            _list.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, _titles);
            _list.Invalidate();
        }
    }
}

