using BolindersBil.Models;
using BolindersBil.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;



namespace BolindersBil.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IHostingEnvironment _hostingEnvironment;
        private IVehicleRepository vehicleRepo;


        public AccountController(IVehicleRepository vehicleRepository, IHostingEnvironment hostingEnvironment, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            vehicleRepo = vehicleRepository;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            // Checks if the user is authenticated/signed in and redirects him/her to Admin: 
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Admin");
            }
            else
            {
                // Otherwise, maybe return to an error page?
                return View();
            }
        }

        // Checks if the password matches to the account, redirects the user to Admin:
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, vm.Password, false, false)).Succeeded)
                    {
                        return RedirectToAction("Admin");
                    }
                }
            }
            return View("Index", vm);
        }

        [Authorize]
        public IActionResult Admin()
        {
            // To get the list of all Vehicles from the repo.
            var getVehicles = vehicleRepo.GetAllVehicles();

            return View(getVehicles);
        }

        // Sends the user back to the login page:
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        // ****TODO: maybe move all the vehicle repo DI and CRUD logic in a Vehicle controller instead.
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
                
                // Taking each uploaded image and saving it in the correct folder structure. 
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

                    // Resize and save the image under the correct folder. Calls on the ImageResize function.
                    string resizedImageFolder = createSpecificVehicleFolder + "\\resized_images";
                    Directory.CreateDirectory(resizedImageFolder);

                    // TODO: figure out how to create the resize folder and have it show up physically first....
                    using (var resizefolder = new FileStream(resizedImageFolder, FileMode.Create)) { }
                    
                    ImageResize(finalTargetFilePath, resizedImageFolder, 100);


                    //// TODO: save the data to the DB tables Image and Vehicle....
                    //var testImage = new Models.Image
                    //{
                    //    Id = addNewVehicle.Id,
                    //    Name = uniqueGuid,
                    //    Path = finalTargetFilePath
                    //};
                    //addNewVehicle.Images = testImage;
                }



                









                //string imageFolder = "Vehicle_Images";
                //string targetFilename = uploadedImage.FileName;
                //// Get the path.
                //string webrootPath = _hostingEnvironment.WebRootPath;
                //string pathOfTargetFolder = webrootPath + "\\images\\" + imageFolder + "\\";
                //string fileTargetOriginal = pathOfTargetFolder + addNewVehicle.RegNr + "\\Original\\" + targetFilename;
                //// Copy file to target.
                //using (var stream = new FileStream(fileTargetOriginal, FileMode.Create))
                //{
                //    await uploadedImage.CopyToAsync(stream);
                //}

                //// Resize and save the image under the folder named: 80. Calls on the ImageResize function.
                //ImageResize(fileTargetOriginal, pathOfTargetFolder + "\\80\\" + targetFilename, 80);

                ////****To save the image to the DB.
                //using (var stream = new MemoryStream())
                //{
                //    await uploadedImage.CopyToAsync(stream);
                //    addNewVehicle.Picture = stream.ToArray();
                //}


                addNewVehicle.AddedDate = DateTime.Now;
                addNewVehicle.UpdatedDate = DateTime.Now;

                vehicleRepo.AddNewVehicle(addNewVehicle);

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
                Picture = vehicle.Picture,
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
        public ActionResult DeleteVehicle()
        {
            return RedirectToAction("Admin");
        }
    }
}
