using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using TSG = Tekla.Structures.Geometry3d;
using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace PnlkArmatura
{
    public partial class Form1 : Form
    {
        TSM.Model MyModel = new TSM.Model();

        double currentPartLength = 0.00;
        double currentPartHeight = 0.00;
        double currentPartWidth = 0.00;



        public Form1()
        {
            InitializeComponent();
        }

        private bool InitializeConnection()
        {
            TSM.Model _model = new TSM.Model();
            if (_model.GetConnectionStatus())
            {
                MyModel = _model;
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!InitializeConnection())
            {
                MessageBox.Show("Подключиться не удалось");
                // this.Close();

               

            }
        }

        // Установить рабочую плоскость 
        private void button1_Click(object sender, EventArgs e)
        {
            TSM.Model myModel = new TSM.Model();

            //The transformation plane to be set as the current transformation plane
            myModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TSM.TransformationPlane());

            //Define origin and two vectors to set the UCS to the XZ plane
            TSG.Point Origin = new TSG.Point(0, 0, 0);  //Represents the origin
            TSG.Vector X = new TSG.Vector(1, 0, 1);     //Represents x-axis
            TSG.Vector Y = new TSG.Vector(0, 1, 1);     //Represents y-axis

            //Create a new transformation plane defined by the given origin and two vectors
            TSM.TransformationPlane XZ_Plane = new TSM.TransformationPlane(Origin, X, Y);

            //Setting the current transformation plane to be (XZ plane)
            myModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(XZ_Plane);
            myModel.CommitChanges();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TSM.Model myModel = new TSM.Model();
            TSM.Beam myBeam = new TSM.Beam(TSM.Beam.BeamTypeEnum.BEAM);

            //Beam myBeam = new Beam(new TSG.Point(1000, 1000, 1000),
            //                        new TSG.Point(6000, 6000, 1000));

            //Beam myBeam = new Beam();
            myBeam.Name = "Хомут";
            myBeam.Material.MaterialString = "C245";
            myBeam.Profile.ProfileString = "100*100";
            myBeam.PartNumber.Prefix = "Деталь";
            myBeam.PartNumber.StartNumber = 1;
            myBeam.AssemblyNumber.Prefix = "Сборка";
            myBeam.AssemblyNumber.StartNumber = 1;
            myBeam.Class = "20";
            myBeam.Position.Plane = TSM.Position.PlaneEnum.MIDDLE;
            myBeam.Position.Depth = TSM.Position.DepthEnum.MIDDLE;
            myBeam.Position.Rotation = TSM.Position.RotationEnum.TOP;
            myBeam.Finish = "";
            myBeam.StartPoint = new TSG.Point(0, 0, 0);
            myBeam.EndPoint = new TSG.Point(1000, 0, 0);


            myBeam.Insert();
            MyModel.CommitChanges();

            if (!myBeam.Insert())
            {
                Console.WriteLine("не добавил");
            }
            if (myBeam.Insert())
            {
                Console.WriteLine("Добавлено!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //clPanel panel1 = new clPanel(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z, "300*100", "C235", "Балка", "1", 5, "Б", 1, "123", "MIDDLE", "MIDDLE", "TOP");
            Convert.ToDouble(txt_PanelWidth.Text);
            Convert.ToString(txt_PanelWidth.Text);
            Convert.ToString(txt_PanelHeight.Text);
            string ProfilPaneli = Convert.ToString(txt_PanelHeight.Text) + "*" + Convert.ToString(txt_PanelWidth.Text);

            cl_CreateConcretPanel panel = new cl_CreateConcretPanel(0, 0, 0, Convert.ToDouble(txt_PanelLength.Text), 0, 0, ProfilPaneli, "B25", "Балка", "1", 5, "Б", 1, "123", "LEFT", "FRONT", "TOP");

            panel.InsertPanel();
            

            ArrayList ObjectsToSelect = new ArrayList();
            ObjectsToSelect.Add(panel);
            //ObjectsToSelect.Add(b3);

            Tekla.Structures.Model.UI.ModelObjectSelector MS = new Tekla.Structures.Model.UI.ModelObjectSelector();
            MS.Select(ObjectsToSelect);

            MyModel.CommitChanges();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Объявляем инициализацию модели для программы.
                TSM.Model MyModel = new TSM.Model();
                // Запоминаем все выделенные детали в модели.
                TSM.ModelObjectEnumerator myEnum = new TSMUI.ModelObjectSelector().GetSelectedObjects();
                // Создаём цикл который будет перебирать все выделенные в модели детали.
                while (myEnum.MoveNext())
                {
                    // Записываем в локальную переменную текущую деталь.
                    TSM.Beam currentPart = myEnum.Current as TSM.Beam;
                    // Проверяем является ли деталь балкой. (не является ли текущая деталь пустой.
                    if (currentPart != null)
                    {

                        if (currentPart.Name == "Панель") // Проверяем свойство "Имя" для детали
                        {
                            // Далее один из вариантов как получить габариты детали.
                            //Перенести рабочую плоскость в начало координат балки
                            //1. Сохранить текущую рабочую плоскость как локальную переменную, чтобы восстановить её позже.
                            TSM.TransformationPlane currentPlane = MyModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();
                            // Получаем локальную рабочую плоскость детали с выделенной балки.
                            TSM.TransformationPlane localPlane = new TSM.TransformationPlane(currentPart.GetCoordinateSystem());
                            // Переносим рабочую плоскость модели на локальную рабочую плоскость детали.
                            MyModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(localPlane);
                            // Запрашиваем фактическую геометрию детали, которую ранее выбрали.
                            TSM.Solid solid = currentPart.GetSolid() as TSM.Solid;
                            double currentPartLength = solid.MaximumPoint.X - solid.MinimumPoint.X;
                            double currentPartHeight = solid.MaximumPoint.Y - solid.MinimumPoint.Y;
                            double currentPartWidth = solid.MaximumPoint.Z - solid.MinimumPoint.Z;

                            MessageBox.Show("" +
                                "currentPartLength  = " + solid.MaximumPoint.X + "-" + solid.MinimumPoint.X + "=" + currentPartLength + "\n" +
                                "currentPartHeight  = " + solid.MaximumPoint.Y + "-" + solid.MinimumPoint.Y + "=" + currentPartHeight + "\n" +
                                "currentPartWidth  = " + solid.MaximumPoint.Z + "-" + solid.MinimumPoint.Z + "=" + currentPartWidth + "\n" +
                                "Test complete!");

                            // Далее записываем нужное нам в пользовательский атрибут "Комментарий (comment)"
                            if (!currentPart.SetUserProperty("comment", "ПН" + currentPartLength + currentPartHeight + currentPartWidth))
                                MessageBox.Show("SetProperty failed!");

                            //Восстановление предыдущей рабочей плоскости.
                            MyModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
                            // Фиксирует изменение внесенные в базу данных модели до этого момента.
                            MyModel.CommitChanges();

                        }

                    }
                }
            }
            catch (Exception W)
            {
                Console.WriteLine("Exception: " + W.ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Вырез по детали
            // Объявляем инициализацию модели для программы.
            TSM.Model MyModel = new TSM.Model();
            //1. Сохранить текущую рабочую плоскость как локальную переменную, чтобы восстановить её позже.
            TSM.TransformationPlane currentPlane = MyModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            // Получаем локальную рабочую плоскость детали с выделенной балки.
            // Указываем профиль в котором будем вырезать
            TSM.UI.Picker firstpick = new TSM.UI.Picker();
            TSM.Part firstp = firstpick.PickObject(TSM.UI.Picker.PickObjectEnum.PICK_ONE_OBJECT, "Выберите объект 1") as TSM.Part;

            TSM.TransformationPlane localPlane = new TSM.TransformationPlane(firstp.GetCoordinateSystem());
            // Переносим рабочую плоскость модели на локальную рабочую плоскость детали.
            MyModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(localPlane);


            TSM.Solid solid1 = firstp.GetSolid() as TSM.Solid;

            // Начальная грань
            double x1 = firstp.GetSolid().MinimumPoint.X;
            double y1 = firstp.GetSolid().MinimumPoint.Y;
            double z1 = firstp.GetSolid().MaximumPoint.Z;

            double x2 = firstp.GetSolid().MinimumPoint.X;
            double y2 = firstp.GetSolid().MinimumPoint.Y;
            double z2 = firstp.GetSolid().MinimumPoint.Z;

            double x3 = firstp.GetSolid().MinimumPoint.X;
            double y3 = firstp.GetSolid().MaximumPoint.Y;
            double z3 = firstp.GetSolid().MaximumPoint.Z;

            double x4 = firstp.GetSolid().MinimumPoint.X;
            double y4 = firstp.GetSolid().MaximumPoint.Y;
            double z4 = firstp.GetSolid().MinimumPoint.Z;

            // Конечная грань
            double x5 = firstp.GetSolid().MaximumPoint.X;
            double y5 = firstp.GetSolid().MinimumPoint.Y;
            double z5 = firstp.GetSolid().MaximumPoint.Z;

            double x6 = firstp.GetSolid().MaximumPoint.X;
            double y6 = firstp.GetSolid().MinimumPoint.Y;
            double z6 = firstp.GetSolid().MinimumPoint.Z;

            double x7 = firstp.GetSolid().MaximumPoint.X;
            double y7 = firstp.GetSolid().MaximumPoint.Y;
            double z7 = firstp.GetSolid().MaximumPoint.Z;

            double x8 = firstp.GetSolid().MaximumPoint.X;
            double y8 = firstp.GetSolid().MaximumPoint.Y;
            double z8 = firstp.GetSolid().MinimumPoint.Z;

            // Создаём профиль по которому будет отсекать.
            Beam Virez = new Beam();
            Virez.Profile.ProfileString = "PEIKKOC160*20";
            Virez.StartPoint = new TSG.Point(x1, y1, z1);
            Virez.EndPoint = new TSG.Point(x3, y3, z3);
            Virez.Position.Plane = TSM.Position.PlaneEnum.RIGHT; // СПРАВА
            Virez.Position.Rotation = TSM.Position.RotationEnum.TOP; // СВЕРХУ
            Virez.Position.Depth = TSM.Position.DepthEnum.BEHIND; // ПОЗАДИ
            Virez.Class = TSM.BooleanPart.BooleanOperativeClassName;
            //clBeamConcrete Virez = new clBeamConcrete(x1, y1, z1, x3, y3, z3, "PEIKKOC100*20", "B25", "Плита", "МОНОЛИТ", 100, "ПБ", 1, "1", "СПРАВА", "СВЕРХУ", "ПОЗАДИ");
            Virez.Insert();


            //TSM.Part Virez2 = new TSM.Part();

            BooleanPart B2 = new BooleanPart();
            // Объявляем Объект главным. Его обрезаем. 
            B2.Father = firstp;
            // Производим операцию вычитания объёмов.
            B2.SetOperativePart(Virez);
            B2.Type = BooleanPart.BooleanTypeEnum.BOOLEAN_CUT; // BOOLEAN_CUT is default type. Чтобы заработало Раскоментил.
            if (!B2.Insert())
                Console.WriteLine("Insert failed!");
            //B2.Delete(); // Not needed when using BOOLEAN_ADD, operative part is deleted automatically. Чтобы заработало Закоментил.

            // Восстановление предыдущей рабочей плоскости.
            MyModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
            MyModel.CommitChanges();
        }

        private void button6_Click(object sender, EventArgs e)
        {
    
            txt_PanelWidth.Text = "200";
            txt_PanelHeight.Text = "2000";
            txt_PanelLength.Text = "4000";
        }
    }

}