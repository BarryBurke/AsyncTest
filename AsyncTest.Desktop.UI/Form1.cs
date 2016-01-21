using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncTest.Desktop.UI
{
    public partial class Form1 : Form
    {
        private ServiceReference1.Service1Client _client;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DownloadImageAsync();
            //GetData(10);

            //var result = GetData1(2);
            //MessageBox.Show(GetData1(2).Result);

            //var client = new ServiceReference1.Service1Client();
            //var result = await client.GetDataAsync(1);
            //label1.Text = result;



            //GetGlobalWeather("Wellington", "New Zealand");

            //Task t = GetGlobalWeatherCities("New Zealand");
            //label1.Text = t.Status.ToString();

            try
            {
                Task<string> wx = GetGlobalWeather1("Edinburgh", "United Kingdom");
                // display the status of the task
                label1.Text = "Task Status:  " + wx.Status.ToString();
                label1.Text += DateTime.Now.ToLongTimeString();
                textBox1.Text = wx.Result;
            }
            catch (Exception ex)
            {

                //With Task<T>, we can handle the error in the in the calling thread

                Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {

                Console.WriteLine("Operation complete, press any key to terminate session");

                Console.ReadLine();

            }
        }

        private void DownloadImage() 
        {
            WebClient client = new WebClient();
            byte[] imageData = client.DownloadData("http://tpstatic.com/img/usermedia/Pr2IapH5Pk2VcdqZr97MlA/cropped-w220-h220.png");
            this.pictureBox1.Image = Image.FromStream(new MemoryStream(imageData));
        }

        private async void DownloadImageAsync()
        {
            WebClient client = new WebClient();
            byte[] imageData = await client.DownloadDataTaskAsync("http://tpstatic.com/img/usermedia/Pr2IapH5Pk2VcdqZr97MlA/cropped-w220-h220.png");
            this.pictureBox1.Image = Image.FromStream(new MemoryStream(imageData));
        }

        private async void GetData(int value)
        {
            var client = new ServiceReference1.Service1Client();
            MessageBox.Show(await client.GetDataAsync(value));

            //var task = client.GetDataAsync(5);
            //string result = await client.GetDataAsync(value);
            //Task<string> data = await client.GetDataAsync(4);
        }

        private async Task<string> GetData1(int value)
        {
            var client = new ServiceReference1.Service1Client();
            return await client.GetDataAsync(value);
        }

        private async void GetGlobalWeather(string city, string country)
        {
            try
            {
                // create an instance of the client
                var client = new WeatherService.GlobalWeatherSoapClient();

                // set a string to the results of the weather query - use await to hold here for the results to
                // be returned to the string variable
                string weather = await client.GetWeatherAsync(city, country);

                textBox1.Text = weather;
                // once the string is populated, write it out to the console
                Console.WriteLine(weather);
                // report completion from the async method

                Console.WriteLine("Service call complete");
            }
            catch (Exception ex)
            {
                // handle any errors here else risk an unhandled exception
                Console.WriteLine("Error:  " + ex.Message);
            }
            finally
            {
                // tell the user to click a key to close the console window
                Console.WriteLine("Press any key to close this window");
            }
        }

        private async Task GetGlobalWeatherCities(string country)
        {
            try
            {
                // create an instance of the client
                var client = new WeatherService.GlobalWeatherSoapClient();

                // set up a variable to hold the result and use await to hold for the service
                // to populate it before pressing on with the task
                string cities = await client.GetCitiesByCountryAsync(country);
                textBox1.Text = cities;

                // once the string variable is populated, print it out to the console
                Console.WriteLine(cities);
            }
            catch (Exception ex)
            {
                // best to handle the exception here if one is encountered
                Console.WriteLine("Error:  " + ex.Message);
            }
        }

        private async Task<string> GetGlobalWeather1(string city, string country)
        {
            // note there is no error handling in the async method on this call, that is not to say 
            // one could not put error handling here but in this case it is possible to catch and handle 
            // errors in the calling thread
            
            // create an instance of the client
            var client = new WeatherService.GlobalWeatherSoapClient();
            
            // we will await the call to fetch the weather and return the result immediately to the caller
            return await client.GetWeatherAsync(city, country);
        }
    }
}
