using System.Reflection.Metadata;
using AppCredenciales.Data;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Presentation;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using static System.Windows.Forms.AxHost;
using System.Text.Json;
using ClosedXML.Excel;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing;
using Path = System.IO.Path;

namespace AppCredenciales
{
    public partial class InitialForm : Form
    {
        List<EmployeeData> data = new List<EmployeeData>();
        List<ExtendedEmployeeD> allData = new List<ExtendedEmployeeD>();
        string imageEmployee = "";

        public InitialForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            if (CheckJSONFile())
            {
                // Si el archivo JSON es válido, cargar los datos en la aplicación
                CheckJSONFile();
            }
            else
            {
                MessageBox.Show("No se pudo cargar el archivo JSON. Por favor, intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string temp_CURP;

        private void comboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxName.SelectedItem != null)
            {
                foreach (var item in allData)
                {
                    if (item.Name == comboBoxName.Text)
                    {
                        temp_CURP = item.CURP;
                        textBoxNSS.Text = item.NSS;
                        textBoxCURP.Text = item.CURP;
                        if (item.ImageBase64 != null)
                        {
                            textBoxPath.Text = "Imagen previamente cargada";
                        }
                        else
                        {
                            textBoxPath.Text = "";
                        }
                    }
                }

            }
        }

        private void ReadCSV()
        {
            string rutaArchivo = "";
            StreamReader sr = new StreamReader("Copy of plantillavidesa 2025.csv", false);
            //string xml = sr.ReadToEnd();
            AutoCompleteStringCollection autoCompleteNames = new AutoCompleteStringCollection();
            sr.ReadLine();
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split(",");
                int ID = int.Parse(line[0]);
                string Nombre = line[1];
                string NSS = line[2];
                string RFC = line[3];
                string CURP = line[4];
                string Fecha = line[5];
                string Departamento = line[6];
                string Puesto = line[7];
                string imgbase64 = null;

                comboBoxName.Items.Add(Nombre);

                EmployeeData obj = new ExtendedEmployeeD(ID, Nombre, NSS, RFC, CURP, Fecha, Departamento, Puesto, imgbase64);
                data.Add(obj);
            }
            sr.Close();
        }

        private void GenerateWorkerCredential()
        {
            try
            {
                // Ruta para guardar el nuevo PDF
                string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string fecha = DateTime.Now.ToString("dd-MM-yyyy");
                string outputPath = Path.Combine(downloadsPath, "Credenciales.pdf");

                string imagePath = Path.Combine(Application.StartupPath, "Img", "CredentialBase.png");

                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 50, 50, 50, 50);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    document.Open();
                    PdfContentByte cb = writer.DirectContent;

                    float availableWidth = 155.91f; // Ancho disponible para el texto

                    for (int i = 0; i < tempList.Count; i++)
                    {
                        float startX = 50;
                        float startY = 550 - (250 * i);

                        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.3f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        cb.SetFontAndSize(baseFont, 8.3f);

                        if (File.Exists(imagePath))
                        {
                            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                            float width = 11f / 2.54f * 72; // 11 cm ancho
                            float height = 8.5f / 2.54f * 72; // 8.5 cm alto
                            image.ScaleAbsolute(width, height);
                            image.SetAbsolutePosition(50, startY);
                            document.Add(image);
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la imagen de la credencial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        int imageWidth = 75;
                        int imageHeight = 75;
                        float positionX = startX + (availableWidth - imageWidth) / 2;
                        var item = tempList[i];

                        if (!string.IsNullOrEmpty(item.ImageBase64))
                        {
                            try
                            {
                                iTextSharp.text.Image imgEmp = iTextSharp.text.Image.GetInstance(item.ImageBase64);
                                imgEmp.ScaleAbsolute(imageWidth, imageHeight);
                                imgEmp.SetAbsolutePosition(positionX, startY + 105); //655
                                document.Add(imgEmp);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error al agregar la imagen del empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("La imagen del empleado está vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        cb.BeginText();

                        // Dividir el nombre en líneas
                        string[] parts = item.Name.Split(' ');
                        string firstLine, secondLine;

                        if (parts.Length > 3)
                        {
                            firstLine = parts[0] + " " + parts[1];
                            secondLine = string.Join(" ", parts.Skip(2));
                        }
                        else
                        {
                            firstLine = parts[0];
                            secondLine = parts[1] + " " + parts[2];
                        }

                        // Centrar la primera línea
                        float textWidth = baseFont.GetWidthPoint(firstLine, 8.3f);
                        float marginLeft = startX + (availableWidth - textWidth) / 2;
                        cb.SetTextMatrix(marginLeft, startY + 90);
                        cb.ShowText(firstLine);

                        // Centrar la segunda línea
                        textWidth = baseFont.GetWidthPoint(secondLine, 8.3f);
                        marginLeft = startX + (availableWidth - textWidth) / 2;
                        cb.SetTextMatrix(marginLeft, startY + 80);
                        cb.ShowText(secondLine);

                        // Centrar el puesto
                        textWidth = baseFont.GetWidthPoint(item.Position, 8f);
                        marginLeft = startX + (availableWidth - textWidth) / 2;
                        cb.SetTextMatrix(marginLeft, startY + 65);
                        cb.ShowText(item.Position);

                        // Centrar el CURP
                        textWidth = baseFont.GetWidthPoint(item.CURP, 8f);
                        marginLeft = startX + (availableWidth - textWidth) / 2;
                        cb.SetTextMatrix(110, startY + 50);
                        cb.ShowText(item.CURP);

                        // Centrar el NSS
                        textWidth = baseFont.GetWidthPoint(item.NSS, 8f);
                        marginLeft = startX + (availableWidth - textWidth) / 2;
                        cb.SetTextMatrix(110, startY + 34);
                        cb.ShowText(item.NSS);


                        cb.EndText();
                    }
                    document.Close();

                    // Actualizar la ruta de la imagen en el JSON
                    UpdateImagePathsInJSON();

                    MessageBox.Show($"Credencial generada correctamente: {outputPath}", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrio un error al generar la credencial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CreateGrid(PdfContentByte cb)
        {
            cb.BeginText();
            //Crear una cuadrícula
            for (int y = 0; y <= 800; y += 50) // Líneas horizontales
            {
                // Dibuja la línea horizontal
                cb.MoveTo(0, y);
                cb.LineTo(600, y);
                cb.Stroke();

                // Agregar texto para Y
                cb.BeginText();
                cb.SetTextMatrix(5, y - 5);
                cb.ShowText($"Y: {y}");
                cb.EndText();
            }

            for (int x = 0; x <= 600; x += 50) // Líneas verticales
            {
                // Dibuja la línea vertical
                cb.MoveTo(x, 0);
                cb.LineTo(x, 800);
                cb.Stroke();

                // Agregar texto para X
                cb.BeginText();
                cb.SetTextMatrix(x + 5, 5);
                cb.ShowText($"X: {x}");
                cb.EndText();
            }
        }

        //private void GenerateWorkerCredential()
        //{
        //    try
        //    {
        //        SaveToJSON();
        //        Ruta para guardar el nuevo PDF
        //        string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        //        string fecha = DateTime.Now.ToString("dd-MM-yyyy");
        //        string outputPath = Path.Combine(downloadsPath, "Credenciales.pdf");
        //        Ruta del PDF generado en Descargas

        //        string imagePath = Path.Combine(Application.StartupPath, "Img", "CredentialBase.png");

        //        string imagePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "");

        //        Ruta de la imagen a

        //        using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        //        {
        //            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 50, 50, 50, 50);
        //            PdfWriter writer = PdfWriter.GetInstance(document, fs);

        //            document.Open();
        //            PdfContentByte cb = writer.DirectContent;
        //            cb.BeginText();

        //            // Crear una cuadrícula
        //            for (int y = 0; y <= 800; y += 50) // Líneas horizontales
        //            {
        //                // Dibuja la línea horizontal
        //                cb.MoveTo(0, y);
        //                cb.LineTo(600, y);
        //                cb.Stroke();

        //                // Agregar texto para Y
        //                cb.BeginText();
        //                cb.SetTextMatrix(5, y - 5);
        //                cb.ShowText($"Y: {y}");
        //                cb.EndText();
        //            }

        //            for (int x = 0; x <= 600; x += 50) // Líneas verticales
        //            {
        //                // Dibuja la línea vertical
        //                cb.MoveTo(x, 0);
        //                cb.LineTo(x, 800);
        //                cb.Stroke();

        //                // Agregar texto para X
        //                cb.BeginText();
        //                cb.SetTextMatrix(x + 5, 5);
        //                cb.ShowText($"X: {x}");
        //                cb.EndText();
        //            }



        //            float availableWidth = 155.91f;

        //            for (int i = 0; i < tempList.Count; i++)
        //            {

        //                float startX = 50;
        //                float startY = 550 - (250 * i);


        //                Agregar texto
        //                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //                iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA,
        //                    8.3f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        //                cb.SetFontAndSize(baseFont, 8.3f);

        //                if (File.Exists(imagePath))
        //                {
        //                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);

        //                    float width = 11f / 2.54f * 72; // 11 cm ancho
        //                    float height = 8.5f / 2.54f * 72; // 8.5 cm a

        //                    image.ScaleAbsolute(width, height);

        //                    image.SetAbsolutePosition(50, startY);
        //                    altura 8.5cm ancho 5.5
        //                    document.Add(image);
        //                }
        //                else
        //                {
        //                    MessageBox.Show("No se encontró la imagen de la credencial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //                imagen empleado
        //                int imageWidth = 75;
        //                int imageHeight = 75;
        //                float positionX = startX + (availableWidth - imageWidth) / 2;
        //                var item = tempList[i];

        //                if (!string.IsNullOrEmpty(item.ImageBase64))
        //                {
        //                    try
        //                    {
        //                        iTextSharp.text.Image imgEmp = iTextSharp.text.Image.GetInstance(item.ImageBase64);
        //                        imgEmp.ScaleAbsolute(imageWidth, imageHeight);
        //                        imgEmp.SetAbsolutePosition(positionX, startY + 105); //655
        //                        document.Add(imgEmp);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        MessageBox.Show($"Error al agregar la imagen del empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("La imagen del empleado está vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }

        //                Pragmatica bold 8.3

        //                cb.BeginText();



        //                Divide el nombre en palabras
        //                string[] parts = item.Name.Split(' ');
        //                string firstLine, secondLine;
        //                float textWidth = 0, marginLeft = 0;

        //                if (parts.Length > 3)
        //                {
        //                    firstLine = parts[0] + " " + parts[1];
        //                    secondLine = string.Join(" ", parts.Skip(2));

        //                    textWidth = baseFont.GetWidthPoint(firstLine, 8.3f);
        //                    marginLeft = positionX;

        //                    cb.SetTextMatrix(marginLeft, startY + 90);
        //                    cb.ShowText(firstLine);

        //                    textWidth = baseFont.GetWidthPoint(secondLine, 11);
        //                    marginLeft = positionX;
        //                    cb.SetTextMatrix(marginLeft, startY + 80);
        //                    cb.ShowText(secondLine);
        //                }
        //                else
        //                {
        //                    firstLine = parts[0];
        //                    secondLine = parts[1] + " " + parts[2];

        //                    textWidth = baseFont.GetWidthPoint(firstLine, 8.3f);
        //                    marginLeft = positionX;
        //                    cb.SetTextMatrix(marginLeft, startY + 90);
        //                    cb.ShowText(firstLine);

        //                    textWidth = baseFont.GetWidthPoint(secondLine, 8.3f);
        //                    marginLeft = positionX;
        //                    cb.SetTextMatrix(marginLeft, startY + 80);
        //                    cb.ShowText(secondLine);
        //                }
        //                cb.SetFontAndSize(baseFont, 8f);
        //                textWidth = baseFont.GetWidthPoint(item.Position, 8f);
        //                marginLeft = positionX;
        //                cb.SetTextMatrix(marginLeft, startY + 63);
        //                cb.ShowText(item.Position);

        //                cb.SetTextMatrix(110, startY + 50);
        //                cb.ShowText(item.CURP);

        //                cb.SetTextMatrix(110, startY + 34);
        //                cb.ShowText(item.NSS);


        //                cb.EndText();


        //            }
        //            document.Close();

        //            UpdateImagePathsInJSON();

        //            MessageBox.Show($"Credencial generada correctamente: {outputPath}", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ocurrio un error al generar la credencial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        List<EmployeeData> tempList = new List<EmployeeData>();
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if an image has been selected
                if (string.IsNullOrEmpty(imageEmployee) || !File.Exists(imageEmployee))
                {
                    MessageBox.Show("La imagen está vacía o no se encontró.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit method if image is invalid
                }

                // Temporarily store the image path in the EmployeeData object
                foreach (var item in allData)
                {
                    if (item.CURP == temp_CURP)
                    {
                        if (tempList.Any(x => x.CURP == item.CURP))
                        {
                            MessageBox.Show($"{item.Name} ya ha sido agregado previamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Store the image path temporarily (do not save it yet)
                        EmployeeData obj = new EmployeeData(item.ID, item.Name, item.NSS, item.CURP,item.Position , imageEmployee);
                        tempList.Add(obj);
                        MessageBox.Show($"Se agregó {item.Name} correctamente");
                        ClearGralVaraibles();
                    }
                }
            }
            catch (Exception ex)
            {
                // Capture any error that occurs during the process
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearGralVaraibles()
        {
            comboBoxName.Text = "";
            textBoxNSS.Text = "";
            textBoxCURP.Text = "";
            textBoxPath.Text = "";
            temp_CURP = "";
            imageEmployee = "";
        }


        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofg = new OpenFileDialog();
            ofg.Title = "Select File";
            ofg.InitialDirectory = @"C:\";
            ofg.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            ofg.ShowDialog();
            imageEmployee = ofg.FileName;
            textBoxPath.Text = ofg.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (tempList.Count == 0)
            {
                MessageBox.Show("No hay empleados agregados para generar credenciales.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Salir del método si no hay datos
            }

            // Open Form2 and pass the tempList (temporary employee data)
            Form2 form2 = new Form2(tempList);
            var result = form2.ShowDialog();

            // Check the result from Form2
            if (result == DialogResult.OK)
            {
                try
                {
                    // Define the assets directory path
                    string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                    string assetsFolderPath = Path.Combine(projectPath, "assets");

                    // Create the assets folder if it doesn't exist
                    if (!Directory.Exists(assetsFolderPath))
                    {
                        Directory.CreateDirectory(assetsFolderPath);
                    }

                    // Loop through all the employees in tempList and save their images
                    foreach (var item in tempList)
                    {
                        // Get employee name and sanitize it (replace spaces with underscores)
                        string sanitizedEmployeeName = item.Name.Replace(" ", "_");
                        string newImageFileName = $"{sanitizedEmployeeName}.jpg";  // You can change the extension based on the image type

                        // Define the new image path inside the assets folder
                        string newImagePath = Path.Combine(assetsFolderPath, newImageFileName);

                        // Copy the image to the assets folder with the new name
                        File.Copy(item.ImageBase64, newImagePath, overwrite: true);

                        // Update the employee's image path in the JSON
                        item.ImageBase64 = Path.Combine("assets", newImageFileName);  // Relative path to save in JSON
                    }

                    // After saving images, generate the credentials
                    GenerateWorkerCredential();

                    // Optionally, save the data back to JSON (if needed)
                    //SaveToJSON();

                    MessageBox.Show("Credenciales generadas correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error al generar las credenciales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (result == DialogResult.Cancel)
            {
                // If the user cancels the operation, show a message
                MessageBox.Show("Proceso cancelado.");
            }
        }

        private void ReadExcelFile(string file)
        {
            using (var workbook = new XLWorkbook(file))
            {
                var worksheet = workbook.Worksheet(1); // Primera hoja
                var rows = worksheet.RangeUsed().RowsUsed(); // Filas con datos

                foreach (var row in rows.Skip(2)) // Omitir encabezados
                {
                    int ID = int.Parse(row.Cell(1).GetString());
                    string Nombre = row.Cell(2).GetString();
                    string NSS = row.Cell(3).GetString();
                    string RFC = row.Cell(4).GetString();
                    string CURP = row.Cell(5).GetString();
                    string Fecha = row.Cell(6).GetString();
                    string Departamento = row.Cell(7).GetString();
                    string Puesto = row.Cell(8).GetString();
                    string imgbase64 = null;

                    ExtendedEmployeeD obj = new ExtendedEmployeeD(ID, Nombre, NSS, RFC, CURP, Fecha, Departamento, Puesto, imgbase64);
                    allData.Add(obj);
                }
            }
        }

        private void SaveToJSON()
        {
            try
            {
                string projectPath = Path.GetDirectoryName(Application.ExecutablePath);
                string jsonFilePath = Path.Combine(projectPath, "EmployeeData.json");

                string json = JsonSerializer.Serialize(allData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonFilePath, json);

                //MessageBox.Show("Datos guardados correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckJSONFile()
        {
            try
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                string jsonFilePath = Path.Combine(projectPath, "EmployeeData.json");

                if (!File.Exists(jsonFilePath))
                {
                    DialogResult result = MessageBox.Show(
                        "La aplicacion no tiene ningun dato cargado. Cargue un archivo Excel",
                        "Datos no encontrados",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Title = "Seleccionar archivo Excel";
                        openFileDialog.Filter = "Archivos Excel|*.xls;*.xlsx";
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ReadExcelFile(openFileDialog.FileName);

                            SaveToJSON();

                            MessageBox.Show("Archivo Excel procesado y guardado correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("No se seleccionó ningún archivo Excel. La aplicación se cerrará.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(0);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("La aplicación se cerrará debido a la falta de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(0);
                        return false;
                    }
                }

                // Si el archivo JSON existe, leer los datos (por ejemplo, para cargarlos en la app)
                string json = File.ReadAllText(jsonFilePath);
                var emp = JsonSerializer.Deserialize<List<ExtendedEmployeeD>>(json);




                if (emp == null || emp.Count == 0)
                {
                    MessageBox.Show("La base de datos esta vacía o tiene datos invalidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                foreach (var employee in emp)
                {
                    comboBoxName.Items.Add(employee.Name);
                }

                allData = emp;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void UpdateImagePathsInJSON()
        {
            try
            {
                // Actualizar la ruta de la imagen en la lista allData
                foreach (var tempItem in tempList)
                {
                    var employee = allData.FirstOrDefault(e => e.CURP == tempItem.CURP);
                    if (employee != null)
                    {
                        employee.ImageBase64 = tempItem.ImageBase64;
                    }
                }

                // Guardar los cambios en el JSON
                SaveToJSON();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar las rutas de las imágenes en el JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                // Abrir cuadro de diálogo para seleccionar el archivo Excel
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Seleccionar archivo Excel";
                openFileDialog.Filter = "Archivos Excel|*.xls;*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Leer los datos del archivo Excel
                    //List<ExtendedEmployeeD> newData = ReadExcelFile(openFileDialog.FileName);

                    // Llamar a la función para actualizar los datos
                    //UpdateDataFromExcel(newData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al actualizar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void UpdateDataFromExcel(List<ExtendedEmployeeD> newData)
        {
            try
            {
                // Diccionario para búsqueda rápida por CURP
                Dictionary<string, ExtendedEmployeeD> existingDataMap = allData.ToDictionary(emp => emp.CURP);
                Dictionary<string, ExtendedEmployeeD> newDataMap = newData.ToDictionary(emp => emp.CURP);

                // Lista temporal para almacenar los datos actualizados
                List<ExtendedEmployeeD> updatedList = new List<ExtendedEmployeeD>();

                // 1. Eliminar registros que no están en el Excel pero sí en el JSON
                foreach (var existingEmp in allData.ToList()) // Usar ToList() para evitar modificar la colección mientras se itera
                {
                    if (!newDataMap.ContainsKey(existingEmp.CURP))
                    {
                        // Eliminar la imagen asociada si existe
                        if (!string.IsNullOrEmpty(existingEmp.ImageBase64))
                        {
                            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, existingEmp.ImageBase64);
                            if (File.Exists(imagePath))
                            {
                                File.Delete(imagePath);
                            }
                        }

                        // Eliminar el registro de la lista principal
                        allData.Remove(existingEmp);
                    }
                }

                // 2. Conservar registros que están en ambos (Excel y JSON) y mantener las imágenes
                foreach (var newEmp in newData)
                {
                    if (existingDataMap.TryGetValue(newEmp.CURP, out var existingEmp))
                    {
                        // Si el empleado ya existe, mantener la imagen previa
                        newEmp.ImageBase64 = existingEmp.ImageBase64;
                    }

                    // Agregar el registro a la lista actualizada
                    updatedList.Add(newEmp);
                }

                // 3. Actualizar la lista principal con los datos nuevos
                allData = updatedList;

                // 4. Guardar los cambios en el JSON
                SaveToJSON();

                MessageBox.Show("Datos actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al actualizar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

