using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop;
using System.Windows.Forms;

namespace TopEditor
{
  class VisDrawer
  {
    //Перед использованием необходимо добавить сслыку на расширение Microsoft.Office.Interop.Visio (project - add reference)
    Microsoft.Office.Interop.Visio.Application VisApp;
    Microsoft.Office.Interop.Visio.Documents MastersDocuments;
    Microsoft.Office.Interop.Visio.Document MasterDoc;
    Microsoft.Office.Interop.Visio.Masters Masters;
    Microsoft.Office.Interop.Visio.Document ActiveDoc;
    Microsoft.Office.Interop.Visio.Page ActivePage;

    public VisDrawer()
    {
      //create the application
      VisApp = new Microsoft.Office.Interop.Visio.Application();
      // add a new document - this becomes the active document
      // if we do not do this we get throw an exception
      VisApp.Documents.Add(@"");
      MastersDocuments = VisApp.Documents;
      // open the page holding the masters collection so we can use it
      // Basic_U.vss is the name of the collection, you could use a different collection
      MasterDoc = MastersDocuments.OpenEx(@"Basic_U.vss", (short)Microsoft.Office.Interop.Visio.VisOpenSaveArgs.visOpenDocked);
      // now get a masters collection to use
      Masters = MasterDoc.Masters;
      // now get the active document
      ActiveDoc = VisApp.ActiveDocument;
      // Create a page to put our shapes on
      ActivePage = ActiveDoc.Pages.Add();

    }

    public void DropShape(string name, double x, double y, double textBoxHeight, int pinDirection)
    {

            // Параметры текстового блока

            // Ширина текстового блока
            double width = 10;

            //Высота текстового блока
            double height = textBoxHeight;

            //Координата x центра текстового блока
            double pinx = 0.0;
            
            //Координата y центра текстового блока
            double piny = 0.0;

            //Коэффициент увеличения ширины блока для того, чтобы вместить текст
            double widhtCoeff = 2.5;//CourierNew(8) = 1.6, Calibri = 

            //Содержимое текстового блока
            string str = name;

            //Размер шрифта
            int fontSize = 12;
            
            //Тип шрифта (обычный = 0, жирный = 17)
            int fontType = 17; 
            
            //Название шрифта
            int fontName = 88;//88 = CourierNew, 4= Calibri

            //Дополнительное увеличение ширины текстового блока для, того чтобы тект уместился в одну строку
            double widthDop = 7.0; //CourierNew = 4.0, Calibri = 


            // Параметры линии со стрелкой

            //Координата х начала линии
            double beginX = 5.0;

            //Координата у начала линии
            double beginY = 5.0;

            //Координата х конца линии
            double endX = 10.0;

            //Координата у конца линии
            double endY = 5.0; 

            //Длина линии
            double lineLength = 15.0;
            
            
            


            // Вычисляем требуемую ширину текстового блока
            width = str.Length*widhtCoeff + widthDop;
            width = Math.Round(width, 0);

            //MessageBox.Show((endX + width / 2.0).ToString());

            //Определяем координаты начала и конца стрелки и координаты центра текстового блока в зависимости от направления порта
            if (pinDirection == 0) //input
            {
                endX = x;
                beginX = endX - lineLength;
                pinx = endX + width / 2.0;
            }
            else if (pinDirection == 1)//output
            {
                beginX = x;
                endX = beginX - lineLength;
                pinx = beginX + width / 2.0;
            }

            endY = beginY = y;
            piny = endY;


            //В соответствии с вычисленными значениями формируем значения ячеек Visio для линии и текстового блока
            string cellWidth = "=" + width.ToString().Replace(",", ".") + "mm";
            string cellHeight = "=" + height.ToString().Replace(",", ".") + "mm";
            string cellPinx = "=" + pinx.ToString().Replace(",",".") + "mm";
            string cellPiny = "=" + piny.ToString().Replace(",", ".") + "mm";

            string cellBeginX = "=" + beginX.ToString().Replace(",", ".") + "mm";
            string cellBeginY = "=" + beginY.ToString().Replace(",", ".") + "mm";
            string cellEndX = "=" + endX.ToString().Replace(",", ".") + "mm";
            string cellEndY = "=" + endY.ToString().Replace(",", ".") + "mm";

            string cellFontSize = "=" + fontSize.ToString() + "pt";
            string cellFontType = "=" + fontType.ToString();
            string cellFontName = "=" + fontName.ToString();

            //MessageBox.Show(cellPinx);

            //Размещаем фигуры с заданными параметрами на листе

            Microsoft.Office.Interop.Visio.Master shapetodrop = Masters.ItemU[@"Rectangle"];
            
            // drop a shape on the page
            Microsoft.Office.Interop.Visio.Shape DropShape = ActivePage.Drop(shapetodrop, x, y);
      // set the name on the shape
      DropShape.Name = name; 
      //now lets set the text on the shape
      DropShape.Text = name;
      
      DropShape.CellsU["PinX"].FormulaU = cellPinx;
      DropShape.CellsU["PinY"].FormulaU = cellPiny;
      DropShape.CellsU["Width"].FormulaU = cellWidth;
      DropShape.CellsU["Height"].FormulaU = cellHeight;


        DropShape.CellsU["Char.Font"].FormulaU = cellFontName;
        DropShape.CellsU["Char.Size"].FormulaU = cellFontSize; 
        DropShape.CellsU["Char.Style"].FormulaU = cellFontType; 
        
            DropShape.FillStyle = "None";
            DropShape.CellsU["Para.HorzAlign"].FormulaU = "= 0"; //0-Alignment left, 1-center, 2 - right

            Microsoft.Office.Interop.Visio.Shape line = ActivePage.DrawLine(0.0, 0.0, 2.0, 2.0);
        line.CellsU["BeginX"].FormulaU = cellBeginX;
        line.CellsU["BeginY"].FormulaU = cellBeginY;
        line.CellsU["EndX"].FormulaU = cellEndX;
        line.CellsU["EndY"].FormulaU = cellEndY;

        //line.CellsU["BeginArrow"].FormulaU = "= 3";
        line.CellsU["EndArrow"].FormulaU = "= 3";

        DropShape.LineStyle = "None"; //Текстовое поле без рамки 



        }

  }
}
