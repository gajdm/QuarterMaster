namespace SoulGames.EasyGridBuilderPro
{
        public enum GridEditorMode //Enums for Grid Mode
        {
                None,                   //Mode not set
                GridLite,               //Lite Mode
                GridPro,                //Pro Mode
        }

        public enum GridAxis //Enums for Grid Axis
        {
                XZ,                     //XZ axis
                XY,                     //XY axis
        }

        public enum GridMode //Enums for Grid Mode
        {
                None,                   //Mode not set
                Build,                  //Building Mode
                Destruct,               //Destruction Mode
                Selected,               //Selected Mode
                Moving,                 //Moving Mode
        }

        public enum BuildableObjectType //Enums for BuildableObjectType
        {
                DefualtObject,          //Default type
                FreeObject,             //Free type
        }

        public enum MouseInputs //Enums for Grid Axis
        {
                LeftButton,             //Left mouse button
                RightButton,            //Right mouse button
                MiddleButton,           //Middle mouse button
                None,                   //No mouse button
        }
}
