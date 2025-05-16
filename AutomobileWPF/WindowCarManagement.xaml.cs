using System;
using System.Windows;
using AutomobileLibrary.Repository;
using AutomobileLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AutomobileWPFApp
{
    public partial class WindowCarManagement : Window
    {
        private ICarRepository carRepository;

        public WindowCarManagement(ICarRepository repository)
        {
            InitializeComponent();
            carRepository = repository;
        }

        private Car GetCarObject()
        {
            Car car = null;
            try
            {
                car = new Car
                {
                    CarId = int.Parse(txtCarId.Text),
                    CarName = txtCarName.Text,
                    Manufacturer = txtManufacturer.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    ReleasedYear = int.Parse(txtReleasedYear.Text)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get car");
            }
            return car;
        }

        public void LoadCarList()
        {
            lvCars.ItemsSource = carRepository.GetCars();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Console.WriteLine(">>> Load button clicked");

                using (var db = new MyStockContext())
                {
                    // Lấy chuỗi kết nối hiện tại từ DbContext
                    var conn = db.Database.GetDbConnection();
                    Console.WriteLine(">>> Connection string: " + conn.ConnectionString);

                    // Mở kết nối thử
                    conn.Open();
                    Console.WriteLine(">>> Database connection successful!");

                    // Lấy dữ liệu từ Cars table
                    var cars = db.Cars.ToList();
                    Console.WriteLine($">>> Total cars loaded: {cars.Count}");

                    lvCars.ItemsSource = cars;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(">>> ERROR: " + ex.Message);
                Console.WriteLine(">>> STACK TRACE: " + ex.StackTrace);
                MessageBox.Show("Error connecting to the database", "Load car list");
            }
            //try
            //{
            //    LoadCarList();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Load car list");
            //}
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Car car = GetCarObject();
                carRepository.InsertCar(car);
                LoadCarList();
                MessageBox.Show($"{car.CarName} inserted successfully", "Insert car");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert car");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Car car = GetCarObject();
                carRepository.UpdateCar(car);
                LoadCarList();
                MessageBox.Show($"{car.CarName} updated successfully", "Update car");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update car");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Car car = GetCarObject();
                carRepository.DeleteCar(car);
                LoadCarList();
                MessageBox.Show($"{car.CarName} deleted successfully", "Delete car");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete car");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => Close();
    }
}
