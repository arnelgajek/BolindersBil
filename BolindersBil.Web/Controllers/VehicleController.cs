using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Models;
using BolindersBil.Web.DataAccess;
using BolindersBil.Data;
using BolindersBil.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Authorization;

namespace BolindersBil.Web.Controllers
{
    public class VehicleController : Controller
    {
        private IVehicleRepository vehicleRepo;
        private IHostingEnvironment _hostingEnvironment;
        public int PageLimit = 8;

        public VehicleController(IVehicleRepository vehicleRepository, IHostingEnvironment hostingEnvironment)
        {
            vehicleRepo = vehicleRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        // Index isn't used for anything important atm but is crutial
        public IActionResult Index()
        {
            return RedirectToAction("VehicleList");
        }

        [HttpPost]
        public IActionResult Search(string searchString, bool Used)
        {
            var searchResults = vehicleRepo.Search(searchString, Used);
            return View("Index", searchResults);
        }
        
        [Authorize]
        public IActionResult Admin()
        {
            // To get the list of all Vehicles from the repo.
            var getVehicles = vehicleRepo.GetAllVehicles();

            return View(getVehicles);
        }

        // Paging
        public IActionResult VehicleList(int page = 1)
        {
            // page = 0 x pagelimit
            var toSkip = (page - 1) * PageLimit;

            // Gets the (pagelimit) amount of vehicles and orders them by their ID
            // This shall be changed to sort by UpdateDate in the future
            var vehicles = vehicleRepo.Vehicles.OrderBy(x => x.Id).Skip(toSkip).Take(PageLimit);

            // Gets new info for the paging. Page becomes 1. (1 (page) x 8 (pagelimit)). 
            // And creates new pages for the amount over the pagelimit (since page becomes 2 then 3...)
            var paging = new PagingInfo
            { CurrentPage = page,
              ItemsPerPage = PageLimit,
              TotalItems = vehicleRepo.Vehicles.Count()
            };

            var vm = new VehiclesSearchViewModel
            {
              Vehicles = vehicles,
              Pager = paging
            };

            return View("Index", vm);
        }
        
        [HttpGet]
        public IActionResult AddNewVehicle()
        {
            // This list is used as the dropdown option in the "Årsmodell" input.
            List<object> years = new List<object>();
            var currentYear = DateTime.Now.Year;
            var theFuture = currentYear + 1;
            years.Add(theFuture);
            var stopYear = 1980;
            for (int y = currentYear; y >= stopYear; y--)
            {
                years.Add(y);
            }
            var seventies = "70-tal";
            var sixties = "60-tal";
            var fifties = "50-tal";
            var superOld = "40-tal eller äldre";
            years.Add(seventies);
            years.Add(sixties);
            years.Add(fifties);
            years.Add(superOld);
            ViewBag.vehicleYearOptions = years;

            // This list is used as the dropdown option in the "Karosstyp" input.
            List<string> bodyType = new List<string>
            {
                "Småbil",
                "Sedan",
                "Halvkombi",
                "Kombi",
                "SUV",
                "Coupé",
                "Cab",
                "Familjebuss",
                "Yrkesfordon"
            };
            ViewBag.bodyTypes = bodyType;

            // This list is used as the dropdown option in the "Bränsletyp" input.
            List<string> fuelType = new List<string>
            {
                "Bensin",
                "Diesel",
                "El",
                "Miljöbränsle/Hybrid"
            };
            ViewBag.fuelTypes = fuelType;

            // This list is used as the dropdown option in the "Anläggning" input.
            List<string> theOffices = new List<string>
            {
                "Jönköping",
                "Värnamo",
                "Göteborg"
            };
            ViewBag.offices = theOffices;

            // This list is used as the dropdown option in the "Växellådstyp" input.
            List<string> gearType = new List<string>
            {
                "Automatisk",
                "Manuell"
            };
            ViewBag.gears = gearType;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewVehicle(Vehicle addNewVehicle, ICollection<IFormFile> uploadedImages)
        {
            if (ModelState.IsValid && addNewVehicle != null)
            {
                // Creating the folder structure.
                string webrootPath = _hostingEnvironment.WebRootPath;
                string createImageFolder = webrootPath + "\\images";
                Directory.CreateDirectory(createImageFolder);
                string createVehicleImagesFolder = createImageFolder + "\\vehicle_images";
                Directory.CreateDirectory(createVehicleImagesFolder);
                string createSpecificVehicleFolder = createVehicleImagesFolder + "\\" + addNewVehicle.Brand + "_" + addNewVehicle.RegNr;
                Directory.CreateDirectory(createSpecificVehicleFolder);

                List<Models.Image> images = new List<Models.Image>();
                
                // Taking each uploaded image and saving it in the correct folder. 
                foreach (var file in uploadedImages)
                {
                    Guid uniqueGuid = Guid.NewGuid();
                    string targetFileName = uniqueGuid + "_" + file.FileName;
                    string finalTargetFilePath = createSpecificVehicleFolder + "\\" + targetFileName;

                    if (file.Length > 0)
                    {
                        // Copy file to target.
                        using (var stream = new FileStream(finalTargetFilePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    
                    var theImage = new Models.Image
                    {
                        Name = uniqueGuid,
                        Path = finalTargetFilePath
                    };
                    images.Add(theImage);
                 
                    // Resize and save the image under the correct folder. Calls on the ImageResize function.
                    string resizedImageFolder = createSpecificVehicleFolder + "\\resized_images";
                    if (!Directory.Exists(resizedImageFolder))
                    {
                        Directory.CreateDirectory(resizedImageFolder);
                    }
                    ImageResize(finalTargetFilePath, resizedImageFolder + "\\" + targetFileName, 100);
                }
                addNewVehicle.Images = images;
                addNewVehicle.AddedDate = DateTime.Now;
                addNewVehicle.UpdatedDate = DateTime.Now;

                vehicleRepo.AddNewVehicle(addNewVehicle);

                // TODO: have a RedirectToAction here and send to the Admin Action method...
                return View("TestVehicleAdded", ViewData);
            }

            return View();
        }

        private void ImageResize(string inputImagePath, string outputImagePath, int newWidth)
        {
            const long quality = 50L;


            Bitmap sourceBitmap = new Bitmap(inputImagePath);
            double dblWidthOriginal = sourceBitmap.Width;
            double dblHeigthOriginal = sourceBitmap.Height;
            double heightWidthRelation = dblHeigthOriginal / dblWidthOriginal;
            int newHeight = (int)(newWidth * heightWidthRelation);

            // Create empty draw area.
            var newDrawArea = new Bitmap(newWidth, newHeight);

            using (var graphic_of_DrawArea = Graphics.FromImage(newDrawArea))
            {
                graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;
                graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;

                // Draw into placeholder.
                // Imports the image into the drawarea.
                graphic_of_DrawArea.DrawImage(sourceBitmap, 0, 0, newWidth, newHeight);

                // Output as .Jpg
                using (var output = System.IO.File.Open(outputImagePath, FileMode.Create))
                {
                    // Setup jpg 
                    var qualityParamId = Encoder.Quality;
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                    // Save Bitmap as Jpg 
                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    newDrawArea.Save(output, codec, encoderParameters);

                    output.Close();
                }

                graphic_of_DrawArea.Dispose();
            }
            sourceBitmap.Dispose();
        }

        [HttpGet]
        public IActionResult EditVehicle(int vehicleId)
        {
            var vehicle = vehicleRepo.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));

            List<string> bodyType = new List<string>
            {
                "Småbil",
                "Sedan",
                "Halvkombi",
                "Kombi",
                "SUV",
                "Coupé",
                "Cab",
                "Familjebuss",
                "Yrkesfordon"
            };

            List<string> gearType = new List<string>
            {
                "Automatisk",
                "Manuell"
            };

            List<string> fuelType = new List<string>
            {
                "Bensin",
                "Diesel",
                "El",
                "Miljöbränsle/Hybrid"
            };

            List<string> theOffices = new List<string>
            {
                "Jönköping",
                "Värnamo",
                "Göteborg"
            };

            var vm = new EditVehicleViewModel()
            {
                Id = vehicle.Id,
                RegNr = vehicle.RegNr,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                ModelDescription = vehicle.ModelDescription,
                Year = vehicle.Year,
                Kilometer = vehicle.Kilometer,
                Price = vehicle.Price,
                Body = vehicle.Body,
                Color = vehicle.Color,
                Gearbox = vehicle.Gearbox,
                Fuel = vehicle.Fuel,
                Horsepower = vehicle.Horsepower,
                Used = vehicle.Used,
                Office = vehicle.Office,
                OfficeId = vehicle.OfficeId,
                Leasable = vehicle.Leasable,
                UpdatedDate = vehicle.UpdatedDate,
                VehicleAttribute = vehicle.VehicleAttribute,
                BodyTypes = bodyType,
                GearTypes = gearType,
                FuelTypes = fuelType,
                Offices = theOffices
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult EditVehicle(EditVehicleViewModel editVehicleViewModel)
        {
            if (ModelState.IsValid)
            {
                editVehicleViewModel.UpdatedDate = DateTime.Now;
                vehicleRepo.UpdateVehicle(editVehicleViewModel);
                return View("TestVehicleAdded");
            }
            else
            {
                // TODO: error message here
                return View(editVehicleViewModel);
            }
        }

        [HttpPost]
        public IActionResult DeleteVehicle(int vehicleId)
        {
            var deleted = vehicleRepo.DeleteVehicle(vehicleId);
            if (deleted != null)
            {
                // Vehicle was found and not deleted...
            }
            else
            {
                //TODO
                // Vehicle was not found - show error
            }
            return RedirectToAction(nameof(Admin));
        }
        
        [HttpPost]
        public ActionResult DeleteVehicle()
        {
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public IActionResult BulkDeleteVehicle(string vehicleId)
        {
            // Creates an array with all the Ids checked to make an BulkDelete:
            int[] bulkDelete = Array.ConvertAll(vehicleId.Split(','), int.Parse);

            // Loops through all the Ids from the array above:
            foreach (var vehicle in bulkDelete)
            {
                // Deletes the vehicles with the specific Ids chosen:
                DeleteVehicle(vehicle);
            }
            // Redirects the user to the account/admin:
            return RedirectToAction(nameof(Admin));
        }
    }
}
