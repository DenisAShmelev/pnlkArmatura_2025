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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TSG = Tekla.Structures.Geometry3d;
using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace PnlkArmatura
{
    public partial class Form1 : Form
    {
        TSM.Model MyModel = new TSM.Model();
        public Form1()
        { InitializeComponent(); }

        private bool InitializeConnection()
        { 
            TSM.Model _model = new TSM.Model();
            if (_model.GetConnectionStatus())
            { MyModel = _model; return true; }
            else
            { return false; } 
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!InitializeConnection())
            { MessageBox.Show("Подключиться не удалось"); }// this.Close();
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

            cl_CreateConcretPanel panel = new cl_CreateConcretPanel(0, 0, 0, Convert.ToDouble(txt_PanelLength.Text), 0, 0, ProfilPaneli, "B25", "Балка", "1", 5, "Б", 1, "123", "LEFT", "BEHIND", "FRONT");
            //[MIDDLE СЕРЕДИНА; LEFT СЛЕВА; RIGHT СПРАВА] ; [MIDDLE СЕРЕДИНА;FRONT СПЕРЕДИ;BEHIND ПОЗАДИ] ; [TOP СВЕРХУ;FRONT СПЕРЕДИ;BELOW СНИЗУ;BACK СЗАДИ]

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




        private void button8_Click(object sender, EventArgs e)
        {
            Convert.ToDouble(txt_PanelWidth.Text);
            Convert.ToString(txt_PanelWidth.Text);
            Convert.ToString(txt_PanelHeight.Text);
            string ProfilPaneli = Convert.ToString(txt_PanelHeight.Text) + "*" + Convert.ToString(txt_PanelWidth.Text);

            cl_CreateConcretPanel panel = new cl_CreateConcretPanel(0, 0, 0, Convert.ToDouble(txt_PanelLength.Text), 0, 0, ProfilPaneli, "B25", "Балка", "1", 5, "Б", 1, "123", "RIGHT", "FRONT", "BACK");
            // [MIDDLE СЕРЕДИНА; LEFT СЛЕВА; RIGHT СПРАВА] ; [MIDDLE СЕРЕДИНА;FRONT СПЕРЕДИ;BEHIND ПОЗАДИ] ; //[TOP СВЕРХУ;FRONT СПЕРЕДИ;BELOW СНИЗУ;BACK СЗАДИ]

            panel.InsertPanel();


            ArrayList ObjectsToSelect = new ArrayList();
            ObjectsToSelect.Add(panel);
            //ObjectsToSelect.Add(b3);

            Tekla.Structures.Model.UI.ModelObjectSelector MS = new Tekla.Structures.Model.UI.ModelObjectSelector();
            MS.Select(ObjectsToSelect);

            MyModel.CommitChanges();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Convert.ToDouble(txt_PanelWidth.Text);
            Convert.ToString(txt_PanelWidth.Text);
            Convert.ToString(txt_PanelHeight.Text);
            string ProfilPaneli = Convert.ToString(txt_PanelHeight.Text) + "*" + Convert.ToString(txt_PanelWidth.Text);

            cl_CreateConcretPanel panel = new cl_CreateConcretPanel(0, 0, 0, Convert.ToDouble(txt_PanelLength.Text), 0, 0, ProfilPaneli, "B25", "Балка", "1", 5, "Б", 1, "123", "LEFT", "FRONT", "BACK");
            // [MIDDLE СЕРЕДИНА; LEFT СЛЕВА; RIGHT СПРАВА] ; [MIDDLE СЕРЕДИНА;FRONT СПЕРЕДИ;BEHIND ПОЗАДИ] ; //[TOP СВЕРХУ;FRONT СПЕРЕДИ;BELOW СНИЗУ;BACK СЗАДИ]

            panel.InsertPanel();


            ArrayList ObjectsToSelect = new ArrayList();
            ObjectsToSelect.Add(panel);
            //ObjectsToSelect.Add(b3);

            Tekla.Structures.Model.UI.ModelObjectSelector MS = new Tekla.Structures.Model.UI.ModelObjectSelector();
            MS.Select(ObjectsToSelect);

            MyModel.CommitChanges();

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Convert.ToDouble(txt_PanelWidth.Text);
            Convert.ToString(txt_PanelWidth.Text);
            Convert.ToString(txt_PanelHeight.Text);
            Convert.ToString(cmb_Position_Plane.Text);
            Convert.ToString(cmb_Position_Depth.Text);
            Convert.ToString(cmb_Position_Rotation.Text);
            Convert.ToString(cb_PanelMaterial.Text);
            Convert.ToString(txt_PanelName.Text);

            string ProfilPaneli = Convert.ToString(txt_PanelHeight.Text) + "*" + Convert.ToString(txt_PanelWidth.Text);

            cl_CreateConcretPanel panel = new cl_CreateConcretPanel(0, 0, 0, Convert.ToDouble(txt_PanelLength.Text), 0, 0, ProfilPaneli, Convert.ToString(cb_PanelMaterial.Text), Convert.ToString(txt_PanelName.Text), "1", 5, "Б", 1, "123", Convert.ToString(cmb_Position_Plane.Text), Convert.ToString(cmb_Position_Depth.Text), Convert.ToString(cmb_Position_Rotation.Text));
            //[MIDDLE СЕРЕДИНА; LEFT СЛЕВА; RIGHT СПРАВА] ; [MIDDLE СЕРЕДИНА;FRONT СПЕРЕДИ;BEHIND ПОЗАДИ] ; [TOP СВЕРХУ;FRONT СПЕРЕДИ;BELOW СНИЗУ;BACK СЗАДИ]

            panel.InsertPanel();


            ArrayList ObjectsToSelect = new ArrayList();
            ObjectsToSelect.Add(panel);
            //ObjectsToSelect.Add(b3);

            Tekla.Structures.Model.UI.ModelObjectSelector MS = new Tekla.Structures.Model.UI.ModelObjectSelector();
            MS.Select(ObjectsToSelect);

            MyModel.CommitChanges();
        }

        private void button9_Click(object sender, EventArgs e)
        {

            TSM.UI.Picker firstpick = new TSM.UI.Picker();
            TSM.Part firstp = firstpick.PickObject(TSM.UI.Picker.PickObjectEnum.PICK_ONE_OBJECT, "Выберите объект 1") as TSM.Part;

            //   TSMUI.Picker picker = new TSMUI.Picker();
            //   ArrayList picker_1 = picker.PickPoints(TSMUI.Picker.PickPointEnum.PICK_FACE, "Укажи грань");

            //1. Сохранить текущую рабочую плоскость как локальную переменную, чтобы восстановить её позже.
            TSM.TransformationPlane currentPlane = MyModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            // Получаем локальную рабочую плоскость детали с выделенной балки.
            TSM.TransformationPlane localPlane = new TSM.TransformationPlane(firstp.GetCoordinateSystem());
            // Переносим рабочую плоскость модели на локальную рабочую плоскость детали.
            MyModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(localPlane);



            //   p1 = picker_1[0] as TSG.Point;
            //   p2 = picker_1[1] as TSG.Point;
            //   p3 = picker_1[2] as TSG.Point;
            //   p4 = picker_1[3] as TSG.Point;

            Beam firstp1 = firstp as Beam;
            //TSM.Solid solid1 = firstp.GetSolid() as TSM.Solid;

            double MinimumX = firstp.GetSolid().MinimumPoint.X;
            double MinimumY = firstp.GetSolid().MinimumPoint.Y;
            double MinimumZ = firstp.GetSolid().MinimumPoint.Z;
            double MaximumX = firstp.GetSolid().MaximumPoint.X;
            double MaximumY = firstp.GetSolid().MaximumPoint.Y;
            double MaximumZ = firstp.GetSolid().MaximumPoint.Z;

            //double MinimumX = p1.X;
            //double MinimumY = p1.Y;
            //double MinimumZ = p1.Z;
            //double MaximumX = p2.X;
            //double MaximumY = p4.Y;
            //double MaximumZ = p4.Z;



            //Polygon Polygon = new Polygon();
            //Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(p1.X, p1.Y, p1.Z));//(MinimumX, MaximumY, MinimumZ));
            //Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(p4.X, p4.Y, p4.Z));//(MinimumX, MinimumY, MinimumZ));

            Polygon Polygon = new Polygon();
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MaximumY, MinimumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MinimumY, MinimumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MinimumY, MaximumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MaximumY, MaximumZ));//(MinimumX, MaximumY, MaximumZ));


            Polygon Polygon2 = new Polygon();
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MaximumY, MinimumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MinimumY, MinimumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MinimumY, MaximumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MaximumY, MaximumZ));

            RebarGroup RebarGroup = new RebarGroup();
            RebarGroup.Polygons.Add(Polygon);
            RebarGroup.Polygons.Add(Polygon2);
            RebarGroup.RadiusValues.Add(40.0);

            // Распределение:
            //RebarGroup.SpacingType = RebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_TARGET_SPACE;
            //RebarGroup.SpacingType = RebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_EXACT_NUMBER; //Распределение - Равномерное распределение по числу стержней (1)
            //RebarGroup.SpacingType = RebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_UNDEFINED;
            RebarGroup.SpacingType = RebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_EXACT_SPACINGS; //Распределение - По точному значению шага (7)
            //RebarGroup.SpacingType = RebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_EXACT_SPACE_FLEX_AT_START;
            //RebarGroup.SpacingType = RebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_EXACT_SPACE_FLEX_AT_BOTH;
            RebarGroup.Spacings.Add(30.0);
            RebarGroup.Spacings.Add(100.0);

            // Исключение стержней:
            //RebarGroup.ExcludeType = RebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_BOTH; //Исключить - Первый и последний
            RebarGroup.ExcludeType = RebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_FIRST; //Исключить - Первый
            //RebarGroup.ExcludeType = RebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_LAST; //Исключить - Последний
            //RebarGroup.ExcludeType = RebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_NONE; //Исключить - Нет (Все стержни показаны)

            RebarGroup.Father = firstp;
            RebarGroup.Name = "RebarGroup";
            RebarGroup.Class = 3;
            RebarGroup.Size = "12";
            RebarGroup.NumberingSeries.StartNumber = 0;
            RebarGroup.NumberingSeries.Prefix = "Group";
            RebarGroup.Grade = "А500С";
            //RebarGroup.StartHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            //RebarGroup.StartHook.Angle = -90;
            //RebarGroup.StartHook.Length = 3;
            //RebarGroup.StartHook.Radius = 20;
            //RebarGroup.EndHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            //RebarGroup.EndHook.Angle = -90;
            //RebarGroup.EndHook.Length = 3;
            //RebarGroup.EndHook.Radius = 20;

            // Защитный слой на плоскости. Добавлять по одному в строке. В программе соединится в одну строку
            RebarGroup.OnPlaneOffsets.Add(25.0);
            RebarGroup.OnPlaneOffsets.Add(10.0);
            RebarGroup.OnPlaneOffsets.Add(25.0);
            RebarGroup.StartPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS; //Защитный слой
            RebarGroup.StartPointOffsetValue = 20; //Защитный слой - Начало
            RebarGroup.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS; //Защитный слой
            //RebarGroup.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_LEG_LENGTH; //Длина участка
            RebarGroup.EndPointOffsetValue = 60; //Защитный слой - Конец
            RebarGroup.FromPlaneOffset = 0; //Защитный слой - От плоскости

            RebarGroup.Insert();

            RebarGroup.Name = "Modified Group 1";
            RebarGroup.Modify();
            MyModel.CommitChanges();

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            Beam Beam = new Beam(new Tekla.Structures.Geometry3d.Point(0, 0, 0), new Tekla.Structures.Geometry3d.Point(1000, 0, 0));
            Beam.Profile.ProfileString = "250*250";
            Beam.Material.MaterialString = "K40-1";
            Beam.Finish = "PAINT";
            Beam.Insert();

            double MinimumX = Beam.GetSolid().MinimumPoint.X;
            double MinimumY = Beam.GetSolid().MinimumPoint.Y;
            double MinimumZ = Beam.GetSolid().MinimumPoint.Z;
            double MaximumX = Beam.GetSolid().MaximumPoint.X;
            double MaximumY = Beam.GetSolid().MaximumPoint.Y;
            double MaximumZ = Beam.GetSolid().MaximumPoint.Z;

            Polygon Polygon = new Polygon();
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MaximumY, MinimumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MinimumY, MinimumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MinimumY, MaximumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MaximumY, MaximumZ));

            Polygon Polygon2 = new Polygon();
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MaximumY, MinimumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MinimumY, MinimumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MinimumY, MaximumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MaximumY, MaximumZ));

            RebarGroup RebarGroup = new RebarGroup();
            RebarGroup.Polygons.Add(Polygon);
            RebarGroup.Polygons.Add(Polygon2);
            RebarGroup.RadiusValues.Add(40.0);
            RebarGroup.SpacingType = RebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_TARGET_SPACE;
            RebarGroup.Spacings.Add(30.0);
            RebarGroup.ExcludeType = RebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_BOTH;
            RebarGroup.Father = Beam;
            RebarGroup.Name = "RebarGroup";
            RebarGroup.Class = 3;
            RebarGroup.Size = "12";
            RebarGroup.NumberingSeries.StartNumber = 0;
            RebarGroup.NumberingSeries.Prefix = "Group";
            RebarGroup.Grade = "А500С";
            RebarGroup.StartHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            RebarGroup.StartHook.Angle = -90;
            RebarGroup.StartHook.Length = 3;
            RebarGroup.StartHook.Radius = 20;
            RebarGroup.EndHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            RebarGroup.EndHook.Angle = -90;
            RebarGroup.EndHook.Length = 3;
            RebarGroup.EndHook.Radius = 20;
            RebarGroup.OnPlaneOffsets.Add(25.0);
            RebarGroup.OnPlaneOffsets.Add(10.0);
            RebarGroup.OnPlaneOffsets.Add(25.0);
            RebarGroup.StartPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            RebarGroup.StartPointOffsetValue = 20;
            RebarGroup.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            RebarGroup.EndPointOffsetValue = 60;
            RebarGroup.FromPlaneOffset = 40;

            RebarGroup.Insert();

            RebarGroup.Name = "Modified Group 1";
            RebarGroup.Modify();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            TSM.UI.Picker firstpick = new TSM.UI.Picker();
            TSM.Part firstp = firstpick.PickObject(TSM.UI.Picker.PickObjectEnum.PICK_ONE_OBJECT, "Выберите объект 1") as TSM.Part;




            TSMUI.Picker picker = new TSMUI.Picker();
            ////ArrayList picker_1 = picker.PickPoints(TSMUI.Picker.PickPointEnum.PICK_FACE, "Укажи грань");

            //1. Сохранить текущую рабочую плоскость как локальную переменную, чтобы восстановить её позже.
            TSM.TransformationPlane currentPlane = MyModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            // Получаем локальную рабочую плоскость детали с выделенной балки.
            TSM.TransformationPlane localPlane = new TSM.TransformationPlane(firstp.GetCoordinateSystem());
            // Переносим рабочую плоскость модели на локальную рабочую плоскость детали.
            MyModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(localPlane);




            Beam firstp1 = firstp as Beam;
            //TSM.Solid solid1 = firstp.GetSolid() as TSM.Solid;

            //Polygon Polygon = new Polygon();
            //Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(p1.X, p1.Y, p1.Z));//(MinimumX, MaximumY, MinimumZ));
            //Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(p4.X, p4.Y, p4.Z));//(MinimumX, MinimumY, MinimumZ));

            Polygon Polygon = new Polygon();
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(0, 0, 0));//(MinimumX, MaximumY, MinimumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(100, 200, 300));//(MinimumX, MinimumY, MinimumZ));
            //Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(p3.X, p3.Y, p3.Z));//(MinimumX, MinimumY, MaximumZ));
            //Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MaximumY, MaximumZ));//(MinimumX, MaximumY, MaximumZ));

            Polygon Polygon2 = new Polygon();
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(0, 0, 0));//(MaximumX, MaximumY, MinimumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(400, 500, 600));//(MaximumX, MinimumY, MinimumZ));
            //Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(p3.X, p3.Y, p3.Z-100));//(MaximumX, MinimumY, MaximumZ));
            //Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MaximumY, MaximumZ));//(MaximumX, MaximumY, MaximumZ));

            RebarGroup RebarGroup = new RebarGroup();
            RebarGroup.Polygons.Add(Polygon);
            RebarGroup.Polygons.Add(Polygon2);
            RebarGroup.RadiusValues.Add(40.0);
            RebarGroup.SpacingType = RebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_TARGET_SPACE;
            RebarGroup.Spacings.Add(30.0);
            RebarGroup.ExcludeType = RebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_BOTH;
            RebarGroup.Father = firstp1;
            RebarGroup.Name = "RebarGroup";
            RebarGroup.Class = 3;
            RebarGroup.Size = "12";
            RebarGroup.NumberingSeries.StartNumber = 0;
            RebarGroup.NumberingSeries.Prefix = "Group";
            RebarGroup.Grade = "А500С";
            RebarGroup.StartHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            RebarGroup.StartHook.Angle = -90;
            RebarGroup.StartHook.Length = 3;
            RebarGroup.StartHook.Radius = 20;
            RebarGroup.EndHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            RebarGroup.EndHook.Angle = -90;
            RebarGroup.EndHook.Length = 3;
            RebarGroup.EndHook.Radius = 20;
            RebarGroup.OnPlaneOffsets.Add(25.0);
            RebarGroup.OnPlaneOffsets.Add(10.0);
            RebarGroup.OnPlaneOffsets.Add(25.0);
            RebarGroup.StartPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            RebarGroup.StartPointOffsetValue = 20;
            RebarGroup.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            RebarGroup.EndPointOffsetValue = 60;
            RebarGroup.FromPlaneOffset = 40;

            RebarGroup.Insert();

            RebarGroup.Name = "Modified Group 1";
            RebarGroup.Modify();
            MyModel.CommitChanges();
        }

        #region Создание арматуры подходящее
        private void button7_Click(object sender, EventArgs e)
        {
            TSM.UI.Picker firstpick = new TSM.UI.Picker();
            TSM.Part firstp = firstpick.PickObject(TSM.UI.Picker.PickObjectEnum.PICK_ONE_OBJECT, "Выберите объект") as TSM.Part;

            //1. Сохранить текущую рабочую плоскость как локальную переменную, чтобы восстановить её позже.
            TSM.TransformationPlane currentPlane = MyModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            // Получаем локальную рабочую плоскость детали с выделенной балки.
            TSM.TransformationPlane localPlane = new TSM.TransformationPlane(firstp.GetCoordinateSystem());
            // Переносим рабочую плоскость модели на локальную рабочую плоскость детали.
            MyModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(localPlane);

            //Beam firstp1 = firstp as Beam;

            double MinimumX = firstp.GetSolid().MinimumPoint.X;
            double MinimumY = firstp.GetSolid().MinimumPoint.Y;
            double MinimumZ = firstp.GetSolid().MinimumPoint.Z;
            double MaximumX = firstp.GetSolid().MaximumPoint.X;
            double MaximumY = firstp.GetSolid().MaximumPoint.Y;
            double MaximumZ = firstp.GetSolid().MaximumPoint.Z;

            Polygon Polygon = new Polygon();
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MaximumY, MinimumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MinimumY, MinimumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MinimumY, MaximumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MaximumY, MaximumZ));

            Polygon Polygon2 = new Polygon();
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MaximumY, MinimumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MinimumY, MinimumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MinimumY, MaximumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MaximumY, MaximumZ));

            RebarGroup RebarGroup = new RebarGroup();
            RebarGroup.Polygons.Add(Polygon);
            RebarGroup.Polygons.Add(Polygon2);
            RebarGroup.RadiusValues.Add(40.0);

            RebarGroup.SpacingType = RebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_EXACT_SPACINGS; //Распределение - По точному значению шага (7)

            #region Разбор строки с шагами чтобы ввести их в теклу
            string input = txt_Step_Spacing.Text; // Исходная строка
            string[] parts = input.Split(' '); // Разделяем строку по пробелам
            foreach (string part in parts) // Проходим по каждому элементу массива
            {
                if (int.TryParse(part, out int number)) // Проверяем, является ли текущий элемент числом
                { RebarGroup.Spacings.Add(Convert.ToDouble(number)); } // Если это число, выводим
                else if (part.Contains("*"))
                {
                    string[] splitByAsterisk = part.Split('*'); // Если элемент содержит символ "*", разделяем его на две части
                    if (int.TryParse(splitByAsterisk[0], out int repeatCount)) // Проверяем, является ли первая часть числом
                    {
                        if (splitByAsterisk.Length > 1) // Если вторая часть существует, повторяем её указанное количество раз
                        {
                            for (int i = 1; i <= Convert.ToInt32(splitByAsterisk[0]); i++)
                            { RebarGroup.Spacings.Add(Convert.ToDouble(splitByAsterisk[1])); }
                        }
                        else { Console.WriteLine($"строка=Ошибка: нет текста после '*' в '{part}'"); }
                    }
                    else { Console.WriteLine($"строка=Ошибка: некорректное число перед '*' в '{part}'"); }
                }
                else { Console.WriteLine($"строка={part}"); } // Если это не число и не содержит "*", выводим как есть
            }
            #endregion


            RebarGroup.ExcludeType = RebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_FIRST; //Исключить - Первый

            RebarGroup.Father = firstp; //Элемент к которому прикрепится арматура
            RebarGroup.Name = "RebarGroup";
            RebarGroup.Class = 500;
            RebarGroup.Size = "12";
            RebarGroup.NumberingSeries.StartNumber = 0;
            RebarGroup.NumberingSeries.Prefix = "Group";
            RebarGroup.Grade = "А500С";

            // Защитный слой на плоскости. Добавлять по одному в строке. В программе соединится в одну строку
            #region Разбор строки с отступами чтобы ввести их в теклу
            string text_iz_OnPlaneOffsets = txt_OnPlaneOffsets.Text; // Исходная строка
            string[] elementi_iz_OnPlaneOffsets = text_iz_OnPlaneOffsets.Split(' '); // Разделяем строку по пробелам
            foreach (string odin_element_iz_OnPlaneOffsets in elementi_iz_OnPlaneOffsets) // Проходим по каждому элементу массива
            {
                if (int.TryParse(odin_element_iz_OnPlaneOffsets, out int number)) // Проверяем, является ли текущий элемент числом
                { RebarGroup.OnPlaneOffsets.Add(Convert.ToDouble(number)); } // Если это число, выводим
                else if (odin_element_iz_OnPlaneOffsets.Contains("*"))
                {
                    string[] elementi_razdelennie_po_zvezdochke = odin_element_iz_OnPlaneOffsets.Split('*'); // Если элемент содержит символ "*", разделяем его на две части
                    if (int.TryParse(elementi_razdelennie_po_zvezdochke[0], out int repeatCount)) // Проверяем, является ли первая часть числом
                    {
                        if (elementi_razdelennie_po_zvezdochke.Length > 1) // Если вторая часть существует, повторяем её указанное количество раз
                        {
                            for (int i = 1; i <= Convert.ToInt32(elementi_razdelennie_po_zvezdochke[0]); i++)
                            { RebarGroup.OnPlaneOffsets.Add(Convert.ToDouble(elementi_razdelennie_po_zvezdochke[1])); }
                        }
                        else { Console.WriteLine($"строка=Ошибка: нет текста после '*' в '{odin_element_iz_OnPlaneOffsets}'"); }
                    }
                    else { Console.WriteLine($"строка=Ошибка: некорректное число перед '*' в '{odin_element_iz_OnPlaneOffsets}'"); }
                }
                else { Console.WriteLine($"строка={odin_element_iz_OnPlaneOffsets}"); } // Если это не число и не содержит "*", выводим как есть
            }
            #endregion

            RebarGroup.StartPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS; //Защитный слой
            RebarGroup.StartPointOffsetValue = 20; //Защитный слой - Начало
            RebarGroup.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS; //Защитный слой
            //RebarGroup.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_LEG_LENGTH; //Длина участка
            RebarGroup.EndPointOffsetValue = 60; //Защитный слой - Конец
            RebarGroup.FromPlaneOffset = 0; //Защитный слой - От плоскости

            RebarGroup.Insert();

            RebarGroup.Name = "Modified Group 1";
            RebarGroup.Modify();
            MyModel.CommitChanges();
        }
        #endregion

        private void button11_Click(object sender, EventArgs e)
        {
            string input = txt_Step_Spacing.Text; // Исходная строка
            string[] parts = input.Split(' '); // Разделяем строку по пробелам
            foreach (string part in parts) // Проходим по каждому элементу массива
            {
                if (int.TryParse(part, out int number)) // Проверяем, является ли текущий элемент числом
                { label4.Text = label4.Text + "\n" + $"число=" + Convert.ToString(number); } // Если это число, выводим
                
                else if (part.Contains("*"))
                {
                    string[] splitByAsterisk = part.Split('*'); // Если элемент содержит символ "*", разделяем его на две части
                    if (int.TryParse(splitByAsterisk[0], out int repeatCount)) // Проверяем, является ли первая часть числом
                    {
                        if (splitByAsterisk.Length > 1) // Если вторая часть существует, повторяем её указанное количество раз
                        {
                            for (int i = 1; i <= Convert.ToInt32(splitByAsterisk[0]); i++)
                            { label4.Text = label4.Text + "\n" + $"строка=" + Convert.ToString(splitByAsterisk[1]); }
                        }
                        else { Console.WriteLine($"строка=Ошибка: нет текста после '*' в '{part}'"); }
                    }
                    else { Console.WriteLine($"строка=Ошибка: некорректное число перед '*' в '{part}'"); }
                }
                else { Console.WriteLine($"строка={part}"); } // Если это не число и не содержит "*", выводим как есть
            }
        }



        private void button6_Click(object sender, EventArgs e)
        {
            txt_PanelWidth.Text = "200";
            txt_PanelHeight.Text = "2860";
            txt_PanelLength.Text = "4000";
            cb_PanelMaterial.Text = "B25";
            txt_PanelName.Text = "Балка";
            cmb_Position_Plane.Text = "LEFT";
            cmb_Position_Depth.Text = "FRONT";
            cmb_Position_Rotation.Text = "BEHIND";

        }

        private void cmb_Position_Depth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            txt_ArmaturaName.Text = "АПВ";
            cb_ArmaturaSort.Text = "А500С";
            cb_ArmaturaDiametr.Text = "12";
            txt_ArmaturaClass.Text = "500";
            txt_ArmaturaZSNachalo.Text = "20";
            txt_ArmaturaZSKonec.Text = "20";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void cb_ArmaturaDiametr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(cb_ArmaturaDiametr.Text) == "6")
            {
                cb_ArmaturaRadiusGiba.Text = "15";
            }
            else if (Convert.ToString(cb_ArmaturaDiametr.Text) == "8")
            {
                cb_ArmaturaRadiusGiba.Text = "20";
            }
            else if (Convert.ToString(cb_ArmaturaDiametr.Text) == "10")
            {
                cb_ArmaturaRadiusGiba.Text = "25";
            }
            else if (Convert.ToString(cb_ArmaturaDiametr.Text) == "12")
            {
                cb_ArmaturaRadiusGiba.Text = "30";
            }
        }

        #region Создание арматуры подходящее
        private void b_CreateArmatura_Click(object sender, EventArgs e)
        {
            TSM.UI.Picker firstpick = new TSM.UI.Picker();
            TSM.Part firstp = firstpick.PickObject(TSM.UI.Picker.PickObjectEnum.PICK_ONE_OBJECT, "Выберите объект") as TSM.Part;

            //1. Сохранить текущую рабочую плоскость как локальную переменную, чтобы восстановить её позже.
            TSM.TransformationPlane currentPlane = MyModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            // Получаем локальную рабочую плоскость детали с выделенной балки.
            TSM.TransformationPlane localPlane = new TSM.TransformationPlane(firstp.GetCoordinateSystem());
            // Переносим рабочую плоскость модели на локальную рабочую плоскость детали.
            MyModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(localPlane);

            //Beam firstp1 = firstp as Beam;

            double MinimumX = firstp.GetSolid().MinimumPoint.X;
            double MinimumY = firstp.GetSolid().MinimumPoint.Y;
            double MinimumZ = firstp.GetSolid().MinimumPoint.Z;
            double MaximumX = firstp.GetSolid().MaximumPoint.X;
            double MaximumY = firstp.GetSolid().MaximumPoint.Y;
            double MaximumZ = firstp.GetSolid().MaximumPoint.Z;

            Polygon Polygon = new Polygon();
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MaximumY, MinimumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MinimumY, MinimumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MinimumY, MaximumZ));
            Polygon.Points.Add(new Tekla.Structures.Geometry3d.Point(MinimumX, MaximumY, MaximumZ));

            Polygon Polygon2 = new Polygon();
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MaximumY, MinimumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MinimumY, MinimumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MinimumY, MaximumZ));
            Polygon2.Points.Add(new Tekla.Structures.Geometry3d.Point(MaximumX, MaximumY, MaximumZ));

            RebarGroup RebarGroup = new RebarGroup();
            RebarGroup.Polygons.Add(Polygon);
            RebarGroup.Polygons.Add(Polygon2);
            RebarGroup.RadiusValues.Add(Convert.ToDouble(cb_ArmaturaRadiusGiba.Text)); 

            RebarGroup.SpacingType = RebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_EXACT_SPACINGS; //Распределение - По точному значению шага (7)

            #region Разбор строки с шагами чтобы ввести их в теклу
            string input = txt_Step_Spacing.Text; // Исходная строка
            string[] parts = input.Split(' '); // Разделяем строку по пробелам
            foreach (string part in parts) // Проходим по каждому элементу массива
            {
                if (int.TryParse(part, out int number)) // Проверяем, является ли текущий элемент числом
                { RebarGroup.Spacings.Add(Convert.ToDouble(number)); } // Если это число, выводим
                else if (part.Contains("*"))
                {
                    string[] splitByAsterisk = part.Split('*'); // Если элемент содержит символ "*", разделяем его на две части
                    if (int.TryParse(splitByAsterisk[0], out int repeatCount)) // Проверяем, является ли первая часть числом
                    {
                        if (splitByAsterisk.Length > 1) // Если вторая часть существует, повторяем её указанное количество раз
                        {
                            for (int i = 1; i <= Convert.ToInt32(splitByAsterisk[0]); i++)
                            { RebarGroup.Spacings.Add(Convert.ToDouble(splitByAsterisk[1])); }
                        }
                        else { Console.WriteLine($"строка=Ошибка: нет текста после '*' в '{part}'"); }
                    }
                    else { Console.WriteLine($"строка=Ошибка: некорректное число перед '*' в '{part}'"); }
                }
                else { Console.WriteLine($"строка={part}"); } // Если это не число и не содержит "*", выводим как есть
            }
            #endregion


            RebarGroup.ExcludeType = RebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_FIRST; //Исключить - Первый

            RebarGroup.Father = firstp; //Элемент к которому прикрепится арматура
            RebarGroup.Name = Convert.ToString(txt_ArmaturaName.Text);
            RebarGroup.Class = Convert.ToInt32(txt_ArmaturaClass.Text);
            RebarGroup.Size = Convert.ToString(cb_ArmaturaDiametr.Text);
            RebarGroup.NumberingSeries.StartNumber = 0;
            RebarGroup.NumberingSeries.Prefix = "Group";
            RebarGroup.Grade = Convert.ToString(cb_ArmaturaSort.Text);

            // Защитный слой на плоскости. Добавлять по одному в строке. В программе соединится в одну строку
            #region Разбор строки с отступами чтобы ввести их в теклу
            string text_iz_OnPlaneOffsets = txt_OnPlaneOffsets.Text; // Исходная строка
            string[] elementi_iz_OnPlaneOffsets = text_iz_OnPlaneOffsets.Split(' '); // Разделяем строку по пробелам
            foreach (string odin_element_iz_OnPlaneOffsets in elementi_iz_OnPlaneOffsets) // Проходим по каждому элементу массива
            {
                if (int.TryParse(odin_element_iz_OnPlaneOffsets, out int number)) // Проверяем, является ли текущий элемент числом
                { RebarGroup.OnPlaneOffsets.Add(Convert.ToDouble(number)); } // Если это число, выводим
                else if (odin_element_iz_OnPlaneOffsets.Contains("*"))
                {
                    string[] elementi_razdelennie_po_zvezdochke = odin_element_iz_OnPlaneOffsets.Split('*'); // Если элемент содержит символ "*", разделяем его на две части
                    if (int.TryParse(elementi_razdelennie_po_zvezdochke[0], out int repeatCount)) // Проверяем, является ли первая часть числом
                    {
                        if (elementi_razdelennie_po_zvezdochke.Length > 1) // Если вторая часть существует, повторяем её указанное количество раз
                        {
                            for (int i = 1; i <= Convert.ToInt32(elementi_razdelennie_po_zvezdochke[0]); i++)
                            { RebarGroup.OnPlaneOffsets.Add(Convert.ToDouble(elementi_razdelennie_po_zvezdochke[1])); }
                        }
                        else { Console.WriteLine($"строка=Ошибка: нет текста после '*' в '{odin_element_iz_OnPlaneOffsets}'"); }
                    }
                    else { Console.WriteLine($"строка=Ошибка: некорректное число перед '*' в '{odin_element_iz_OnPlaneOffsets}'"); }
                }
                else { Console.WriteLine($"строка={odin_element_iz_OnPlaneOffsets}"); } // Если это не число и не содержит "*", выводим как есть
            }
            #endregion

            RebarGroup.StartPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS; //Защитный слой
            RebarGroup.StartPointOffsetValue = 20; //Защитный слой - Начало
            RebarGroup.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS; //Защитный слой
            //RebarGroup.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_LEG_LENGTH; //Длина участка
            RebarGroup.EndPointOffsetValue = 60; //Защитный слой - Конец
            RebarGroup.FromPlaneOffset = 0; //Защитный слой - От плоскости

            RebarGroup.Insert();

            RebarGroup.Name = Convert.ToString(txt_ArmaturaName.Text);
            RebarGroup.Modify();
            MyModel.CommitChanges();
        }
        #endregion

        private void b_CreatePanel_Click(object sender, EventArgs e)
        {
            Convert.ToDouble(txt_PanelWidth.Text);
            Convert.ToString(txt_PanelWidth.Text);
            Convert.ToString(txt_PanelHeight.Text);
            Convert.ToString(cmb_Position_Plane.Text);
            Convert.ToString(cmb_Position_Depth.Text);
            Convert.ToString(cmb_Position_Rotation.Text);
            Convert.ToString(cb_PanelMaterial.Text);
            Convert.ToString(txt_PanelName.Text);

            string ProfilPaneli = Convert.ToString(txt_PanelHeight.Text) + "*" + Convert.ToString(txt_PanelWidth.Text);

            cl_CreateConcretPanel panel = new cl_CreateConcretPanel(0, 0, 0, Convert.ToDouble(txt_PanelLength.Text), 0, 0, ProfilPaneli, Convert.ToString(cb_PanelMaterial.Text), Convert.ToString(txt_PanelName.Text), "1", 5, "Б", 1, "123", Convert.ToString(cmb_Position_Plane.Text), Convert.ToString(cmb_Position_Depth.Text), Convert.ToString(cmb_Position_Rotation.Text));
            //[MIDDLE СЕРЕДИНА; LEFT СЛЕВА; RIGHT СПРАВА] ; [MIDDLE СЕРЕДИНА;FRONT СПЕРЕДИ;BEHIND ПОЗАДИ] ; [TOP СВЕРХУ;FRONT СПЕРЕДИ;BELOW СНИЗУ;BACK СЗАДИ]

            panel.InsertPanel();


            ArrayList ObjectsToSelect = new ArrayList();
            ObjectsToSelect.Add(panel);
            //ObjectsToSelect.Add(b3);

            Tekla.Structures.Model.UI.ModelObjectSelector MS = new Tekla.Structures.Model.UI.ModelObjectSelector();
            MS.Select(ObjectsToSelect);

            MyModel.CommitChanges();
        }



        private void button13_Click(object sender, EventArgs e)
        {
            // Задаёмся точкой с будущим началом координат плоскости.
            TSG.Point point0 = new TSG.Point(1000.00, 1000.00, 0.00);

            

            TSG.Point origin0 = new TSG.Point(0.00, 0.00, 0.00);
            TSG.Point origin1 = new TSG.Point(100.00, 0.00, 0.00);
            TSG.Point origin2 = new TSG.Point(0.00, 100.00, 0.00);


            // Задаём векторами указывающими направление осей X и Y (задаёмся двумя точками, и из координат конца, вычитаем координаты начала)
            TSG.Vector vectorX = new TSG.Vector(origin1.X - origin0.X, origin1.Y - origin0.Y, origin1.Z - origin0.Z);
            TSG.Vector vectorY = new TSG.Vector(origin2.X - origin0.X, origin2.Y - origin0.Y, origin2.Z - origin0.Z);

            // Объявляем плоскость на основании точки и двух векторов.
            TSM.TransformationPlane newPlane = new TSM.TransformationPlane(point0, vectorX, vectorY);

            // Переносим рабочую плоскость модели на новую рабочую плоскость.
            MyModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(newPlane);
            MyModel.CommitChanges();
        }
    }
}