using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using OpenQA.Selenium.Interactions;

class Program
{
    static void Main()
    {
        try
        {
            var op = new EdgeOptions();
            var Dv = new EdgeDriver(op);

            var screenshoot = Directory.GetFiles(Directory.GetCurrentDirectory(), ".png");

            Dv.Manage().Window.Maximize();

            // Buscar la página.
            Dv.Navigate().GoToUrl("https://www.nh-hotels.com/es");

            System.Threading.Thread.Sleep(4000);

            CaptureScreenshot(Dv, "pagina_inicial");

            System.Threading.Thread.Sleep(1000);


            var consentElement = Dv.FindElement(By.Id("consent-prompt-submit"));
            if (consentElement.Displayed)
            {
                consentElement.Click();
            }

            System.Threading.Thread.Sleep(2000);
            //Iniciar seccion
            //Entra al icono
            Console.WriteLine("la sucia esa 2");
            var login = Dv.FindElement(By.CssSelector("a.custom-btn.user"));
            Console.WriteLine("la sucia esa 3");
            login.Click();
            Console.WriteLine("Meroly no se baña");

            CaptureScreenshot(Dv, "perfil");
            System.Threading.Thread.Sleep(5000);

            var no = Dv.FindElement(By.Id("login-email"));
            Console.WriteLine("meroly no se baña 2");
            no.SendKeys("20221017@itla.edu.do");
            CaptureScreenshot(Dv, "Email_Ingresado");

            System.Threading.Thread.Sleep(1000);


            Console.WriteLine("meroly no se baña 3");
            //Coloca la contrasena
            var Contra = Dv.FindElement(By.Id("login-password"));
            Contra.SendKeys("Mh20221017*");
            Console.WriteLine("meroly no se baña 4");
            System.Threading.Thread.Sleep(1000);

            CaptureScreenshot(Dv, "contrasena_ingresada");

            //Presiona el boton
            var Botonlogin = Dv.FindElement(By.CssSelector("button.btn.btn-primary.btn-submit"));
            Console.WriteLine("meroly no se baña 5");
            Botonlogin.Click();
            Console.WriteLine("meroly no se baña 6");

            System.Threading.Thread.Sleep(4000);
            CaptureScreenshot(Dv, "Entrar");

            try
            {
                // Localizar el botón sin la clase is-open y hacer clic
                var botonSinOpenClass = Dv.FindElement(By.CssSelector("a.navigation-link:not(.is-open)"));
                botonSinOpenClass.Click();
                CaptureScreenshot(Dv, "abriendo subpanel");
                Console.WriteLine("Subpanel abierto");

                // Aquí puedes interactuar con los elementos dentro del subpanel si es necesario

                // Ahora, el mismo botón tiene la clase is-open
                // Puedes volver a hacer clic para cerrar el subpanel
                botonSinOpenClass.Click();
                CaptureScreenshot(Dv, "cerrando subpanel");
                Console.WriteLine("Subpanel cerrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("meroly no se baña 8");
            Dv.Navigate().GoToUrl("https://www.nh-hotels.com/es");
            System.Threading.Thread.Sleep(4000);
            CaptureScreenshot(Dv, "dirigiendo");
            //Buscar

            Console.WriteLine("meroly no se baña 9");



            //Cerrar sesion
            //Entrar al icono
            var IconoCerrar = Dv.FindElement(By.CssSelector("h-full"));
            ExecuteJavaScriptClick(Dv, IconoCerrar);

            System.Threading.Thread.Sleep(1000);
            CaptureScreenshot(Dv, "Cerrar Sesion Icono");

            //Presionar el boton
            var Cerrar = Dv.FindElement(By.CssSelector("flex w-full items-center h-full  cursor-pointer hover:bg-grey-200 px-4"));
            ExecuteJavaScriptClick(Dv, Cerrar);

            System.Threading.Thread.Sleep(1000);
            CaptureScreenshot(Dv, "Cerrar sesion");
        }


        catch (Exception x)
        {
            Console.WriteLine("Error en", x.Message);
        }
        finally
        {
            CreateHtmlReport();
        }
    }

    static void ExecuteJavaScriptClick(IWebDriver Dv, IWebElement element)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)Dv;
        js.ExecuteScript("arguments[0].click();", element);
    }

    static void CaptureScreenshot(IWebDriver driver, string screenshotName)
    {
        // Tomar captura de pantalla y guardarla en la carpeta actual
        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        screenshot.SaveAsFile($"{screenshotName}.png", ScreenshotImageFormat.Png);
    }

    static void CreateHtmlReport()
    {
        // Obtener la lista de archivos de capturas de pantalla en la carpeta actual
        var screenshots = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.png");

        // Crear el contenido HTML del informe
        var htmlContent = "<html><body>";
        foreach (var screenshot in screenshots)
        {
            htmlContent += $"<img src=\"{screenshot}\" alt=\"screenshot\"><br>";
        }
        htmlContent += "</body></html>";

        // Guardar el contenido HTML en un archivo
        File.WriteAllText("report.html", htmlContent);

        // Obtener la ruta completa del informe HTML
        var reportPath = Path.GetFullPath("report.html");

        // Abrir automáticamente el informe HTML usando el programa predeterminado asociado
        System.Diagnostics.Process.Start(new ProcessStartInfo(reportPath) { UseShellExecute = true });
    }
}