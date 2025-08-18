using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM = Tekla.Structures.Model;
using TSG = Tekla.Structures.Geometry3d;
using Tekla.Structures;
using Tekla.Structures.Model;

namespace PnlkArmatura
{
    class cl_CreateConcretPanel
    {
        TSM.Beam panel = new TSM.Beam(TSM.Beam.BeamTypeEnum.PANEL);

        //public clBeam(double pX1, double pY1, double pZ1, double pX2, double pY2, double pZ2, string Profile, string Material, string Name, string Part_Prefix, int Part_StartNumber, string Assembly_Prefix, int Assembly_StartNumber, string Class, string Position_Plane, string Position_Rotation, string Position_Depth)
        public cl_CreateConcretPanel(double pX1, double pY1, double pZ1, double pX2, double pY2, double pZ2, string Profile, string Material, string Name, string Part_Prefix, int Part_StartNumber, string Assembly_Prefix, int Assembly_StartNumber, string Class, string Position_Plane, string Position_Rotation, string Position_Depth)
        {

            panel.Profile.ProfileString = Profile;
            panel.Material.MaterialString = Material;
            panel.Name = Name;
            panel.PartNumber.Prefix = Part_Prefix;
            panel.PartNumber.StartNumber = Part_StartNumber;
            panel.AssemblyNumber.Prefix = Assembly_Prefix;
            panel.AssemblyNumber.StartNumber = Assembly_StartNumber;
            panel.Class = Class;
            panel.StartPoint = new TSG.Point(pX1, pY1, pZ1);
            panel.EndPoint = new TSG.Point(pX2, pY2, pZ2);

            switch (Position_Plane.ToUpper())
            {
                //[MIDDLE;LEFT;RIGHT]
                case "MIDDLE":
                    panel.Position.Plane = TSM.Position.PlaneEnum.MIDDLE; // середина
                    break;
                case "СЕРЕДИНА":
                    panel.Position.Plane = TSM.Position.PlaneEnum.MIDDLE; // середина
                    break;

                case "LEFT":
                    panel.Position.Plane = TSM.Position.PlaneEnum.LEFT; // слева
                    break;
                case "СЛЕВА":
                    panel.Position.Plane = TSM.Position.PlaneEnum.LEFT; // слева
                    break;

                case "RIGHT":
                    panel.Position.Plane = TSM.Position.PlaneEnum.RIGHT; // справа
                    break;
                case "СПРАВА":
                    panel.Position.Plane = TSM.Position.PlaneEnum.RIGHT; // справа
                    break;

                default:
                    panel.Position.Plane = TSM.Position.PlaneEnum.MIDDLE; // середина
                    break;
            }

            switch (Position_Depth.ToUpper())
            {
                //[MIDDLE СЕРЕДИНА;FRONT СПЕРЕДИ;BEHIND ПОЗАДИ]
                case "MIDDLE":
                    panel.Position.Depth = TSM.Position.DepthEnum.MIDDLE; // середина
                    break;
                case "СЕРЕДИНА":
                    panel.Position.Depth = TSM.Position.DepthEnum.MIDDLE; // середина
                    break;

                case "FRONT":
                    panel.Position.Depth = TSM.Position.DepthEnum.FRONT; // спереди
                    break;
                case "СПЕРЕДИ":
                    panel.Position.Depth = TSM.Position.DepthEnum.FRONT; // спереди
                    break;

                case "BEHIND":
                    panel.Position.Depth = TSM.Position.DepthEnum.BEHIND; // позади
                    break;
                case "ПОЗАДИ":
                    panel.Position.Depth = TSM.Position.DepthEnum.BEHIND; // позади
                    break;

                default:
                    panel.Position.Depth = TSM.Position.DepthEnum.MIDDLE;
                    break;
            }

            switch (Position_Rotation.ToUpper())
            {
                //[TOP СВЕРХУ;FRONT СПЕРЕДИ;BELOW СНИЗУ;BACK СЗАДИ]
                case "TOP":
                    panel.Position.Rotation = TSM.Position.RotationEnum.TOP; // сверху
                    break;
                case "СВЕРХУ":
                    panel.Position.Rotation = TSM.Position.RotationEnum.TOP; // сверху
                    break;

                case "FRONT":
                    panel.Position.Rotation = TSM.Position.RotationEnum.FRONT; // спереди
                    break;
                case "СПЕРЕДИ":
                    panel.Position.Rotation = TSM.Position.RotationEnum.FRONT; // спереди
                    break;

                case "BELOW":
                    panel.Position.Rotation = TSM.Position.RotationEnum.BELOW; // снизу
                    break;
                case "СНИЗУ":
                    panel.Position.Rotation = TSM.Position.RotationEnum.BELOW; // снизу
                    break;

                case "BACK":
                    panel.Position.Rotation = TSM.Position.RotationEnum.BACK; // сзади
                    break;
                case "СЗАДИ":
                    panel.Position.Rotation = TSM.Position.RotationEnum.BACK; // сзади
                    break;

                default:
                    panel.Position.Rotation = TSM.Position.RotationEnum.TOP;
                    break;
            }





            //   panel.Profile.ProfileString = "100*100";
            //   panel.Material.MaterialString = "C245";
            //    panel.Name = "Beam";
            //    panel.PartNumber.Prefix = "B";
            //    panel.PartNumber.StartNumber = 1;
            //   panel.AssemblyNumber.Prefix = "N";
            //   panel.AssemblyNumber.StartNumber = 1;
            //    panel.Class = "20";
            //    panel.Position.Plane = TSM.Position.PlaneEnum.MIDDLE;
            //   panel.Position.Depth = TSM.Position.DepthEnum.MIDDLE;
            //   panel.Position.Rotation = TSM.Position.RotationEnum.FRONT;
            //    panel.StartPoint = new TSG.Point(pX1, pY1, pZ1);
            //    panel.EndPoint = new TSG.Point(pX2, pY2, pZ2);
        }

        public void InsertPanel()
        {
            panel.Insert();
        }
    }
}
