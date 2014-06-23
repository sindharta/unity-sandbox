using ProtoBuf.Meta;
using GameData;
using System.Diagnostics;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataSerializer {

    class Program {
    
        static void Main(string[] args)
        {
            var model = TypeModel.Create();

            model.Add(typeof(HeroData), true);
            model.Add(typeof(GameDatabase), true);
            model.Add(typeof(Color), true);
            model.Add(typeof(Vector3), true);

            model.AllowParseableTypes = true;
            model.AutoAddMissingTypes = true;

            string output_file = "GameDataSerializer.dll";
            if (File.Exists(output_file)) {
                try {
                    File.Delete(output_file);
                } catch (UnauthorizedAccessException) {
                    System.Diagnostics.Debug.WriteLine("No permission to delete " + output_file);
                }
            }
            model.Compile("GameDataSerializer", output_file);

            string final_destination = "../../../../Assets/Plugins/DLL/" + output_file;
            File.Copy(output_file,final_destination,true);
            System.Diagnostics.Debug.Write("Serializer DLL successfully created in " + final_destination);
        }
    }

}

