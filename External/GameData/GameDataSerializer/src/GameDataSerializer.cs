using ProtoBuf.Meta;
using GameData;
using System.Diagnostics;
using System.IO;
using System;

namespace GameDataSerializer {

    class Program {
    
        static void Main(string[] args)
        {
            var model = TypeModel.Create();

            model.Add(typeof(HeroData), true);
            model.Add(typeof(HeroDataDic), true);

            model.AllowParseableTypes = true;
            model.AutoAddMissingTypes = true;

            string output_file = "GameDataSerializer.dll";
            if (File.Exists(output_file)) {
                try {
                    File.Delete(output_file);
                } catch (UnauthorizedAccessException) {
                    Debug.WriteLine("No permission to delete " + output_file);
                }
            }
            model.Compile("GameDataSerializer", output_file);

            string final_destination = "../../../../Assets/Plugins/DLL/" + output_file;
            File.Copy(output_file,final_destination,true);
        }
    }

}

