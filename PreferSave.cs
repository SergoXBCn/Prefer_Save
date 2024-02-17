using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TESTDLL
{
   
    public struct ItemString
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public ItemString(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
    public struct ItemInt
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public ItemInt(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
    public struct ItemLong
    {
        public string Name { get; set; }
        public long Value { get; set; }
        public ItemLong(string name, long value)
        {
            Name = name;
            Value = value;
        }
    }
    public struct ItemBool
    {
        public string Name { get; set; }
        public bool Value { get; set; }
        public ItemBool(string name, bool value)
        {
            Name = name;
            Value = value;
        }
    }
    public struct ItemDecimal
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public ItemDecimal(string name, decimal value)
        {
            Name = name;
            Value = value;
        }
    }
    public struct ItemFloat
    {
        public string Name { get; set; }
        public float  Value { get; set; }
        public ItemFloat(string name, float value)
        {
            Name = name;
            Value = value;
        }
    }
    public struct ItemDouble
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public ItemDouble(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
    //структуры пипов данных
    public struct mString
    {
        public string Name { get; set; }
        public string SyncOrNot { get; set; }
        public string Path { get; set; }
        public string IndexClass { get; set; }
        public string Type { get; set; }
      
    }
    public struct mInt
    {
        public string Name { get; set; }
        public string SyncOrNot { get; set; }
        public string Path { get; set; }
        public string IndexClass { get; set; }
        public string Type { get; set; }

    }
    public struct mBool
    {
        public string Name { get; set; }
        public string SyncOrNot { get; set; }
        public string Path { get; set; }
        public string IndexClass { get; set; }
        public string Type { get; set; }

    }
    public struct mLong
    {
        public string Name { get; set; }
        public string SyncOrNot { get; set; }
        public string Path { get; set; }
        public string IndexClass { get; set; }
        public string Type { get; set; }

    }
    public struct mDecimal
    {
        public string Name { get; set; }
        public string SyncOrNot { get; set; }
        public string Path { get; set; }
        public string IndexClass { get; set; }
        public string Type { get; set; }

    }
    public struct mFloat
    {
        public string Name { get; set; }
        public string SyncOrNot { get; set; }
        public string Path { get; set; }
        public string IndexClass { get; set; }
        public string Type { get; set; }

    }
    public struct mDouble
    {
        public string Name { get; set; }
        public string SyncOrNot { get; set; }
        public string Path { get; set; }
        public string IndexClass { get; set; }
        public string Type { get; set; }

    }
    public static class SuperParams
    {
        public static int CountClass = 0;
        public static List<List<ItemString>> SuperStringList = new List<List<ItemString>>();
        public static List<List<ItemInt>> SuperIntList = new List<List<ItemInt>>();
        public static List<List<ItemLong>> SuperLongList = new List<List<ItemLong>>();
        public static List<List<ItemBool>> SuperBoolList = new List<List<ItemBool>>();
        public static List<List<ItemDecimal>> SuperDecimalList = new List<List<ItemDecimal>>();
        public static List<List<ItemFloat>> SuperFloatList = new List<List<ItemFloat>>();
        public static List<List<ItemDouble>> SuperDoubleList = new List<List<ItemDouble>>();
        public static List<bool> disposable = new List<bool>();
     
    }
    public class PreferSave
    {
        private string path;
        private string asyncWrite = "sync";
        string text;
        int textlength;
        int IndexClass;
        public bool AsyncWrite
        {
            get
            {
                if (asyncWrite == "async")
                {
                    return true;
                }
                return false;
            }
            set {
                if (value == true)
                {
                    asyncWrite = "async";
                }
                else
                {
                    asyncWrite = "sync";
                }
            }
        }


       
        public PreferSave(string FilePath)
        {
            SuperParams.disposable.Add(true);
            SuperParams.SuperStringList.Add(new List<ItemString>());
            SuperParams.SuperIntList.Add(new List<ItemInt>());
            SuperParams.SuperLongList.Add(new List<ItemLong>());
            SuperParams.SuperBoolList.Add(new List<ItemBool>());
            SuperParams.SuperDecimalList.Add(new List<ItemDecimal>());
            SuperParams.SuperFloatList.Add(new List<ItemFloat>());
            SuperParams.SuperDoubleList.Add(new List<ItemDouble>());
            IndexClass = SuperParams.CountClass;
            SuperParams.CountClass++;

            path = FilePath;  
            //Если файла нет, создай
            if (!File.Exists(path))
            {
                try
                {
                    File.Create(path);
                }
                catch (Exception e) { Console.WriteLine(e.Message); }

            }
            //записть всего файла в строку
            try
            {
                text = ReadFile(path);
                textlength = text.Length;
            }
            catch { }
            /*
             здесь подымется весь файл и данные сортируются по листам
             */
              Packing();


        }
        private void Packing()
        {
            /*
              здесь все данные фасуются по листам 
                с соответствующим типом даннных

                <string [3]name="abc" [5]value="asdfg"/>
                <int [3]name="abc" [5]value="12345"/>
            */
            //string name = "string";
            
            string[] mytypes = { "int", "string", "bool", "long", "decimal", "float", "double" };
            int typescr = 0;
            string type = "";
            string originaltype = "";
            string buffer = "";
            int buf = 0;
            string newtext = "";
           
            string mtName = "";
            string mtValue = "";

            for (int i = 0; i < textlength; i++)
            {
                try
                {

                    if (text[i] == '<')
                    {
                        i++;
                    metka:;
                        string dantype = mytypes[typescr];
                        int dantypelength = dantype.Length;
                        type = text.Substring(i, dantypelength);
                        if (type == dantype)
                        {
                            originaltype = dantype;
                        }
                        else
                        {
                            typescr++;
                            goto metka;
                        }
                        
                        // <string [10]name="abcdefghij" [5]value="asdfg"/>
                        i += dantypelength + 1;
                    metka2:;
                        for (int I = i; I < textlength; I++)
                        {
                            if (text[I] == '[')
                            {
                                I++;
                                i = I;
                                newtext = text.Substring(I);
                                int where = newtext.IndexOf(']');
                                buffer = newtext.Substring(0, where);
                                buf = int.Parse(buffer);
                                i += buffer.Length + 1;
                                I = i;
                            }
                            string mName = "name";
                            string mValue = "value";
                            int mNamelength = mName.Length;
                            int mValuelength = mValue.Length;

                            if (mtName == "" || mtValue == "")
                            {
                                if (text.Substring(I, mNamelength) == mName)
                                {
                                    I += mNamelength + 2;
                                    i = I;
                                    mtName = text.Substring(I, buf);
                                    I += mNamelength + 1;
                                    i = I;
                                    goto metka2;



                                }
                                if (text.Substring(I, mValuelength) == mValue)
                                {
                                    I += mValuelength + 2;
                                    i = I;
                                    mtValue = text.Substring(I, buf);
                                    I += mValuelength + 1;
                                    i = I;
                                    goto metka2;


                                }
                            }
                            else
                            {
                                break;
                            }


                        }

                        if (originaltype == "int")
                        {
                            ItemInt item = new ItemInt(mtName, int.Parse(mtValue));
                            SuperParams.SuperIntList[IndexClass].Add(item);
                         
                        }
                        if (originaltype == "string")
                        {
                            ItemString item = new ItemString(mtName, mtValue);
                            SuperParams.SuperStringList[IndexClass].Add(item);
                            
                        }
                        if (originaltype == "bool")
                        {
                            bool my = false;
                            if (mtValue == "true")
                            {
                                my = true;
                            }
                            ItemBool item = new ItemBool(mtName, my);
                            SuperParams.SuperBoolList[IndexClass].Add(item);
                        }
                        if (originaltype == "long")
                        {
                            ItemLong item = new ItemLong(mtName, long.Parse(mtValue));
                            SuperParams.SuperLongList[IndexClass].Add(item);
                        }
                        if (originaltype == "decimal")
                        {
                            ItemDecimal item = new ItemDecimal(mtName, decimal.Parse(mtValue));
                            SuperParams.SuperDecimalList[IndexClass].Add(item);
                        }
                        if (originaltype == "float")
                        {
                            ItemFloat item = new ItemFloat(mtName, float.Parse(mtValue));
                            SuperParams.SuperFloatList[IndexClass].Add(item);
                        }
                        if (originaltype == "double")
                        {
                            ItemDouble item = new ItemDouble(mtName, double.Parse(mtValue));
                            SuperParams.SuperDoubleList[IndexClass].Add(item);
                        }
                        mtName = "";
                        mtValue = "";
                        originaltype = "";
                        typescr = 0;



                    }
                }
                catch { }


            }
            SuperParams.disposable[IndexClass] = false;

        }
        public void Dispose()
        {
            SuperParams.disposable[IndexClass] = true;
        }
        public void SetNewPath(string path)
        {
            this.path = path;
        }
        public mString String(string name)
        {
            mString ms = new mString();
            ms.Name = name;
            ms.SyncOrNot = asyncWrite;
            ms.Path = path;
            ms.IndexClass = IndexClass.ToString();
            ms.Type = "string";
            return ms;
        }      
        public mInt Int(string name)
        {
            mInt mi = new mInt();
            mi.Name = name;
            mi.SyncOrNot = asyncWrite;
            mi.Path = path;
            mi.IndexClass = IndexClass.ToString();
            mi.Type = "int";
            return mi;
        }
        public mLong Long(string name)
        {
            mLong ml = new mLong();
            ml.Name = name;
            ml.SyncOrNot = asyncWrite;
            ml.Path = path;
            ml.IndexClass = IndexClass.ToString();
            ml.Type = "long";
            return ml;
        }
        public mBool Bool(string name)
        {
            mBool mb = new mBool();
            mb.Name = name;
            mb.SyncOrNot = asyncWrite;
            mb.Path = path;
            mb.IndexClass = IndexClass.ToString();
            mb.Type = "bool";
            return mb;
        }
        public mDecimal Decimal(string name)
        {
            mDecimal md = new mDecimal();
            md.Name = name;
            md.SyncOrNot = asyncWrite;
            md.Path = path;
            md.IndexClass = IndexClass.ToString();
            md.Type = "decimal";
            return md;
        }
        public mFloat Float(string name)
        {
            mFloat mf = new mFloat();
            mf.Name = name;
            mf.SyncOrNot = asyncWrite;
            mf.Path = path;
            mf.IndexClass = IndexClass.ToString();
            mf.Type = "float";
            return mf;
        }
        public mDouble Double(string name)
        {
            mDouble md = new mDouble();
            md.Name = name;
            md.SyncOrNot = asyncWrite;
            md.Path = path;
            md.IndexClass = IndexClass.ToString();
            md.Type = "double";
            return md;
        }
        private string ReadFile(string path)
        {
            string File = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    File = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch { }
            return File;
        }
        public bool HasString(string name)
        {
            CheckedResouse(path, IndexClass);
            for (int i = 0; i<SuperParams.SuperStringList[IndexClass].Count; i++)
            {
                if (SuperParams.SuperStringList[IndexClass][i].Name==name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasInt(string name)
        {
            CheckedResouse(path, IndexClass);
            for (int i = 0; i < SuperParams.SuperIntList[IndexClass].Count; i++)
            {
                if (SuperParams.SuperIntList[IndexClass][i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasBool(string name)
        {
            CheckedResouse(path, IndexClass);
            for (int i = 0; i < SuperParams.SuperBoolList[IndexClass].Count; i++)
            {
                if (SuperParams.SuperBoolList[IndexClass][i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasLong(string name)
        {
            CheckedResouse(path, IndexClass);
            for (int i = 0; i < SuperParams.SuperLongList[IndexClass].Count; i++)
            {
                if (SuperParams.SuperLongList[IndexClass][i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasDecimal(string name)
        {
            CheckedResouse(path, IndexClass);
            for (int i = 0; i < SuperParams.SuperDecimalList[IndexClass].Count; i++)
            {
                if (SuperParams.SuperDecimalList[IndexClass][i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasFloat(string name)
        {
            CheckedResouse(path, IndexClass);
            for (int i = 0; i < SuperParams.SuperFloatList[IndexClass].Count; i++)
            {
                if (SuperParams.SuperFloatList[IndexClass][i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasDouble(string name)
        {
            CheckedResouse(path, IndexClass);
            for (int i = 0; i < SuperParams.SuperDoubleList[IndexClass].Count; i++)
            {
                if (SuperParams.SuperDoubleList[IndexClass][i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasName(string name)
        {
            bool bl = false;
            if (HasBool(name))
            { bl = true;}
            if (HasString(name))
            { bl = true; }
            if (HasInt(name))
            { bl = true; }
            if (HasDecimal(name))
            { bl = true; }
            if (HasLong(name))
            { bl = true; }
            if (HasFloat(name))
            { bl = true; }
            if (HasDouble(name))
            { bl = true; }
            return bl;
        }
       
        private void CheckedResouse(string path, int IndexClass)
        {
            //если ресурсы очищены
            if (SuperParams.disposable[IndexClass])
            {
                if (!File.Exists(path))
                {
                    try
                    {
                        File.Create(path);
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }

                }
                else
                {
                    Packing();
                }
              
            }
        }
        public void DeleteFile()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        

    }
    public static class FileWorker
    {
       
        public static async void Set(this mString ms, string text)
        {
            string name = ms.Name;  
            string type = ms.Type;
            string path = ms.Path;
            string myType = ms.Type;
            string syncOrAsync = ms.SyncOrNot;
            int IndexClass = int.Parse(ms.IndexClass);
            CheckedResouse(path, IndexClass);
            bool nal = false;
                int index = 0;
                for (int i = 0; i < SuperParams.SuperStringList[IndexClass].Count; i++)
                {
                    string Name = SuperParams.SuperStringList[IndexClass][i].Name;

                    if (Name == name)
                    {
                        nal = true;
                        ItemString item = new ItemString(Name, text);
                        SuperParams.SuperStringList[IndexClass][i] = item;
                        // тут ещё надо перезаписать файл
                        // длинна StringList пока не изменена ведь item не добавлялся
                        break;
                    }
                }
                if (!nal)
                {
                    ItemString item = new ItemString(name, text);
                    SuperParams.SuperStringList[IndexClass].Add(item);
                }
                try
                {
                    if (syncOrAsync == "async")
                    {
                        //тут АСИНХРОННАЯ запись/перезапись
                        if (!nal)
                        {   //если преременная nal до сих пор false то такого имени там не содержится
                            //можем просто навсего добавить новую строчку в конец файла и всё
                            //это путь
                            try
                            {
                                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                                {
                                    string symv = @"/>";
                                    string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{text.Length.ToString()}]value=\"{text}\"{symv}";
                                    await sw.WriteLineAsync(now);
                                    sw.Close();
                                }
                            }
                            catch { }
                            //это мы дописали в конец файла совершенно новую строку
                        }
                        else
                        {
                        // тут увы надо перезаписать весь файл, причём асинхронно, причём все данные из листов

                        string NewFile = SuperNewFile(IndexClass);

                        try
                        {
                                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                                {
                                    await sw.WriteLineAsync(NewFile);
                                    sw.Close();
                                }
                            }
                            catch { }

                        }

                    }
                    else
                    {
                        // тут синхронная запись/перезаписть
                        if (!nal)
                        {
                            try
                            {
                                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                                {
                                    string symv = @"/>";
                                    string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{text.Length.ToString()}]value=\"{text}\"{symv}";
                                    sw.WriteLine(now);
                                    sw.Close();
                                }
                            }
                            catch { }

                        }
                        else
                        {
                        string NewFile = SuperNewFile(IndexClass);

                        try
                        {
                                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                                {
                                    sw.WriteLine(NewFile);
                                    sw.Close();
                                }
                            }
                            catch { }

                        }
                    }
                }
                catch { }

            

        }
        public static async void Set(this mInt mi, int value)
        {
        
            string name = mi.Name;
            string type = mi.Type;
            string path = mi.Path;
            string myType = mi.Type;
            string syncOrAsync = mi.SyncOrNot;
            int IndexClass = int.Parse(mi.IndexClass);
            CheckedResouse(path, IndexClass);
            bool nal = false;
          
                for (int i = 0; i < SuperParams.SuperIntList[IndexClass].Count; i++)
                {
                    string Name = SuperParams.SuperIntList[IndexClass][i].Name;

                    if (Name == name)
                    {
                        nal = true;
                        ItemInt item = new ItemInt(Name, value);
                        SuperParams.SuperIntList[IndexClass][i] = item;
                        // тут ещё надо перезаписать файл
                        // длинна StringList пока не изменена ведь item не добавлялся
                        break;
                    }
                }
                if (!nal)
                {
                    ItemInt item = new ItemInt(name, value);
                    SuperParams.SuperIntList[IndexClass].Add(item);
                    // тут ещё надо перезаписать файл
                    // длтна StringList уже изменена так как элемент уже добавился
                }
                try
                {
                    if (syncOrAsync == "async")
                    {
                        //тут АСИНХРОННАЯ запись/перезапись
                        if (!nal)
                        {   //если преременная nal до сих пор false то такого имени там не содержится
                            //можем просто навсего добавить новую строчку в конец файла и всё
                            //это путь
                            try
                            {
                                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                                {
                                    string symv = @"/>";
                                    string now = $"<int [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString()}\"{symv}";
                                    await sw.WriteLineAsync(now);
                                    sw.Close();
                                }
                            }
                            catch { }
                            //это мы дописали в конец файла совершенно новую строку
                        }
                        else
                        {
                            // тут увы надо перезаписать весь файл, причём асинхронно, причём все данные из листов

                            string NewFile = SuperNewFile(IndexClass);


                        try
                        {
                                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                                {
                                    await sw.WriteLineAsync(NewFile);
                                    sw.Close();
                                }
                            }
                            catch { }

                        }

                    }
                    else
                    {
                        // тут синхронная запись/перезаписть
                        if (!nal)
                        {
                            try
                            {
                                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                                {
                                    string symv = @"/>";
                                    string now = $"<int [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString()}\"{symv}";
                                    sw.WriteLine(now);
                                    sw.Close();
                                }
                            }
                            catch { }

                        }
                        else
                        {
                        string NewFile = SuperNewFile(IndexClass);


                            

                            try
                            {
                                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                                {
                                    sw.WriteLine(NewFile);
                                    sw.Close();
                                }
                            }
                            catch { }

                        }
                    }
                }
                catch { }
        }
        public static async void Set(this mLong ml, long value)
        {
            string name = ml.Name;
            string path = ml.Path;
            string myType = ml.Type;
            string syncOrAsync = ml.SyncOrNot;
            int IndexClass = int.Parse(ml.IndexClass);
            CheckedResouse(path, IndexClass);
            bool nal = false;
            int index = 0;
            for (int i = 0; i < SuperParams.SuperLongList[IndexClass].Count; i++)
            {
                string Name = SuperParams.SuperLongList[IndexClass][i].Name;

                if (Name == name)
                {
                    nal = true;
                    ItemLong item = new ItemLong(Name, value);
                    SuperParams.SuperLongList[IndexClass][i] = item;
                    // тут ещё надо перезаписать файл
                    // длинна StringList пока не изменена ведь item не добавлялся
                    break;
                }
            }
            if (!nal)
            {
                ItemLong item = new ItemLong(name, value);
                SuperParams.SuperLongList[IndexClass].Add(item);
                // тут ещё надо перезаписать файл
                // длтна StringList уже изменена так как элемент уже добавился
            }
            try
            {
                if (syncOrAsync == "async")
                {
                    //тут АСИНХРОННАЯ запись/перезапись
                    if (!nal)
                    {   //если преременная nal до сих пор false то такого имени там не содержится
                        //можем просто навсего добавить новую строчку в конец файла и всё
                        //это путь
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                            {
                                string symv = @"/>";
                                string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString()}\"{symv}";
                                await sw.WriteLineAsync(now);
                                sw.Close();
                            }
                        }
                        catch { }
                        //это мы дописали в конец файла совершенно новую строку
                    }
                    else
                    {
                        // тут увы надо перезаписать весь файл, причём асинхронно, причём все данные из листов
                        string NewFile = SuperNewFile(IndexClass);

                        try
                        {
                            using (StreamWriter sw = new StreamWriter(name, false, System.Text.Encoding.UTF8))
                            {
                                await sw.WriteLineAsync(NewFile);
                                sw.Close();
                            }
                        }
                        catch { }

                    }

                }
                else
                {
                    // тут синхронная запись/перезаписть
                    if (!nal)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(name, true, System.Text.Encoding.UTF8))
                            {
                                string symv = @"/>";
                                string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString()}\"{symv}";
                                sw.WriteLine(now);
                                sw.Close();
                            }
                        }
                        catch { }

                    }
                    else
                    {
                        string NewFile = SuperNewFile(IndexClass);


                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                            {
                                sw.WriteLine(NewFile);
                                sw.Close();
                            }
                        }
                        catch { }

                    }
                }
            }
            catch { }

        }
        public static async void Set(this mBool mb, bool value)
        {
            string name = mb.Name;
            string path = mb.Path;
            string myType = mb.Type;
            string syncOrAsync = mb.SyncOrNot;
            int IndexClass = int.Parse(mb.IndexClass);
            CheckedResouse(path, IndexClass);
            bool nal = false;
     
                for (int i = 0; i < SuperParams.SuperBoolList[IndexClass].Count; i++)
                {
                    string Name = SuperParams.SuperBoolList[IndexClass][i].Name;

                    if (Name == name)
                    {
                        nal = true;
                        ItemBool item = new ItemBool(Name, value);
                        SuperParams.SuperBoolList[IndexClass][i] = item;
                        // тут ещё надо перезаписать файл
                        // длинна StringList пока не изменена ведь item не добавлялся
                        break;
                    }
                }
                if (!nal)
                {
                    ItemBool item = new ItemBool(name, value);
                    SuperParams.SuperBoolList[IndexClass].Add(item);
                    // тут ещё надо перезаписать файл
                    // длтна StringList уже изменена так как элемент уже добавился
                }
                try
                {
                    if (syncOrAsync == "async")
                    {
                        //тут АСИНХРОННАЯ запись/перезапись
                        if (!nal)
                        {   //если преременная nal до сих пор false то такого имени там не содержится
                            //можем просто навсего добавить новую строчку в конец файла и всё
                            //это путь
                            try
                            {
                                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                                {
                                    string symv = @"/>";
                                    string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString().ToLower()}\"{symv}";
                                    await sw.WriteLineAsync(now);
                                    sw.Close();
                                }
                            }
                            catch { }
                            //это мы дописали в конец файла совершенно новую строку
                        }
                        else
                        {
                        // тут увы надо перезаписать весь файл, причём асинхронно, причём все данные из листов

                        string NewFile = SuperNewFile(IndexClass);


                        try
                        {
                                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                                {
                                    await sw.WriteLineAsync(NewFile);
                                    sw.Close();
                                }
                            }
                            catch { }

                        }

                    }
                    else
                    {
                        // тут синхронная запись/перезаписть
                        if (!nal)
                        {
                            try
                            {
                                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                                {
                                    string symv = @"/>";
                                    string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString().ToLower()}\"{symv}";
                                    sw.WriteLine(now);
                                    sw.Close();
                                }
                            }
                            catch { }

                        }
                        else
                        {
                              string NewFile = SuperNewFile(IndexClass);


                        try
                        {
                                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                                {
                                    sw.WriteLine(NewFile);
                                    sw.Close();
                                }
                        }
                            catch { }

                        }
                    }
                }
                catch { }
            
        }
        public static async void Set(this mDecimal md, decimal value)
        { 
            string name = md.Name;
            string path = md.Path;
            string myType = md.Type;
            string syncOrAsync = md.SyncOrNot;
            int IndexClass = int.Parse(md.IndexClass);
            CheckedResouse(path, IndexClass);
            bool nal = false;
            for (int i = 0; i < SuperParams.SuperDecimalList[IndexClass].Count; i++)
            {
                string Name = SuperParams.SuperDecimalList[IndexClass][i].Name;

                if (Name == name)
                {
                    nal = true;
                    ItemDecimal item = new ItemDecimal(Name, value);
                    SuperParams.SuperDecimalList[IndexClass][i] = item;
                    // тут ещё надо перезаписать файл
                    // длинна StringList пока не изменена ведь item не добавлялся
                    break;
                }
            }
            if (!nal)
            {
                ItemDecimal item = new ItemDecimal(name, value);
                SuperParams.SuperDecimalList[IndexClass].Add(item);
                // тут ещё надо перезаписать файл
                // длтна StringList уже изменена так как элемент уже добавился
            }
            try
            {
                if (syncOrAsync == "async")
                {
                    //тут АСИНХРОННАЯ запись/перезапись
                    if (!nal)
                    {   //если преременная nal до сих пор false то такого имени там не содержится
                        //можем просто навсего добавить новую строчку в конец файла и всё
                        //это путь
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                            {
                                string symv = @"/>";
                                string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString()}\"{symv}";
                                await sw.WriteLineAsync(now);
                                sw.Close();
                            }
                        }
                        catch { }
                        //это мы дописали в конец файла совершенно новую строку
                    }
                    else
                    {
                        // тут увы надо перезаписать весь файл, причём асинхронно, причём все данные из листов

                        string NewFile = SuperNewFile(IndexClass);


                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                            {
                                await sw.WriteLineAsync(NewFile);
                                sw.Close();
                            }
                        }
                        catch { }

                    }

                }
                else
                {
                    // тут синхронная запись/перезаписть
                    if (!nal)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                            {
                                string symv = @"/>";
                                string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString()}\"{symv}";
                                sw.WriteLine(now);
                                sw.Close();
                            }
                        }
                        catch { }

                    }
                    else
                    {
                        string NewFile = SuperNewFile(IndexClass);


                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                            {
                                sw.WriteLine(NewFile);
                                sw.Close();
                            }
                        }
                        catch { }

                    }
                }
            }
            catch { }
        }
        public static async void Set(this mFloat mf, float value)
        {
            string name = mf.Name;
            string path = mf.Path;
            string myType = mf.Type;
            string syncOrAsync = mf.SyncOrNot;
            int IndexClass = int.Parse(mf.IndexClass);
            CheckedResouse(path, IndexClass);
            bool nal = false;
            for (int i = 0; i < SuperParams.SuperFloatList[IndexClass].Count; i++)
            {
                string Name = SuperParams.SuperFloatList[IndexClass][i].Name;

                if (Name == name)
                {
                    nal = true;
                    ItemFloat item = new ItemFloat(Name, value);
                    SuperParams.SuperFloatList[IndexClass][i] = item;
                    // тут ещё надо перезаписать файл
                    // длинна StringList пока не изменена ведь item не добавлялся
                    break;
                }
            }
            if (!nal)
            {
                ItemFloat item = new ItemFloat(name, value);
                SuperParams.SuperFloatList[IndexClass].Add(item);
                // тут ещё надо перезаписать файл
                // длтна StringList уже изменена так как элемент уже добавился
            }
            try
            {
                if (syncOrAsync == "async")
                {
                    //тут АСИНХРОННАЯ запись/перезапись
                    if (!nal)
                    {   //если преременная nal до сих пор false то такого имени там не содержится
                        //можем просто навсего добавить новую строчку в конец файла и всё
                        //это путь
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                            {
                                string symv = @"/>";
                                string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString()}\"{symv}";
                                await sw.WriteLineAsync(now);
                                sw.Close();
                            }
                        }
                        catch { }
                        //это мы дописали в конец файла совершенно новую строку
                    }
                    else
                    {
                        // тут увы надо перезаписать весь файл, причём асинхронно, причём все данные из листов

                        string NewFile = SuperNewFile(IndexClass);


                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                            {
                                await sw.WriteLineAsync(NewFile);
                                sw.Close();
                            }
                        }
                        catch { }

                    }

                }
                else
                {
                    // тут синхронная запись/перезаписть
                    if (!nal)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                            {
                                string symv = @"/>";
                                string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString()}\"{symv}";
                                sw.WriteLine(now);
                                sw.Close();
                            }
                        }
                        catch { }

                    }
                    else
                    {
                        string NewFile = SuperNewFile(IndexClass);


                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                            {
                                sw.WriteLine(NewFile);
                                sw.Close();
                            }
                        }
                        catch { }

                    }
                }
            }
            catch { }
        }
        public static async void Set(this mDouble md, double value)
        {
            string name = md.Name;
            string path = md.Path;
            string myType = md.Type;
            string syncOrAsync = md.SyncOrNot;
            int IndexClass = int.Parse(md.IndexClass);
            CheckedResouse(path, IndexClass);
            bool nal = false;
            for (int i = 0; i < SuperParams.SuperDoubleList[IndexClass].Count; i++)
            {
                string Name = SuperParams.SuperDoubleList[IndexClass][i].Name;

                if (Name == name)
                {
                    nal = true;
                    ItemDouble item = new ItemDouble(Name, value);
                    SuperParams.SuperDoubleList[IndexClass][i] = item;
                    // тут ещё надо перезаписать файл
                    // длинна StringList пока не изменена ведь item не добавлялся
                    break;
                }
            }
            if (!nal)
            {
                ItemDouble item = new ItemDouble(name, value);
                SuperParams.SuperDoubleList[IndexClass].Add(item);
                // тут ещё надо перезаписать файл
                // длтна StringList уже изменена так как элемент уже добавился
            }
            try
            {
                if (syncOrAsync == "async")
                {
                    //тут АСИНХРОННАЯ запись/перезапись
                    if (!nal)
                    {   //если преременная nal до сих пор false то такого имени там не содержится
                        //можем просто навсего добавить новую строчку в конец файла и всё
                        //это путь
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                            {
                                string symv = @"/>";
                                string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString()}\"{symv}";
                                await sw.WriteLineAsync(now);
                                sw.Close();
                            }
                        }
                        catch { }
                        //это мы дописали в конец файла совершенно новую строку
                    }
                    else
                    {
                        // тут увы надо перезаписать весь файл, причём асинхронно, причём все данные из листов

                        string NewFile = SuperNewFile(IndexClass);


                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                            {
                                await sw.WriteLineAsync(NewFile);
                                sw.Close();
                            }
                        }
                        catch { }

                    }

                }
                else
                {
                    // тут синхронная запись/перезаписть
                    if (!nal)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                            {
                                string symv = @"/>";
                                string now = $"<{myType} [{name.Length.ToString()}]name=\"{name}\" [{value.ToString().Length.ToString()}]value=\"{value.ToString()}\"{symv}";
                                sw.WriteLine(now);
                                sw.Close();
                            }
                        }
                        catch { }

                    }
                    else
                    {
                        string NewFile = SuperNewFile(IndexClass);

                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                            {
                                sw.WriteLine(NewFile);
                                sw.Close();
                            }
                        }
                        catch { }

                    }
                }
            }
            catch { }


        }

        private static string SoberiStroky(List<ItemInt> list)
        {
            string NewFile = "";
            for (int i = 0; i < list.Count; i++)
            {
                string symv = @"/>";
                string newname = list[i].Name;
                string newvalue = list[i].Value.ToString();
                string now = $"<int [{newname.Length}]name=\"{newname}\" [{newvalue.Length}]value=\"{newvalue}\"{symv}";
                NewFile += now + "\n";
            }
            return NewFile;
        }
        private static string SoberiStroky(List<ItemString> list)
        {
            string NewFile = "";
            for (int i = 0; i < list.Count; i++)
            {
                string symv = @"/>";
                string newname = list[i].Name;
                string newvalue = list[i].Value;
                string now = $"<string [{newname.Length}]name=\"{newname}\" [{newvalue.Length}]value=\"{newvalue}\"{symv}";
                NewFile += now + "\n";
            }
            return NewFile;
        }
        private static string SoberiStroky(List<ItemBool> list)
        {
            string NewFile = "";
            for (int i = 0; i < list.Count; i++)
            {
                string symv = @"/>";
                string newname = list[i].Name;
                bool newvalue = list[i].Value;
                string now = $"<bool [{newname.Length}]name=\"{newname}\" [{newvalue.ToString().ToLower().Length}]value=\"{newvalue.ToString().ToLower()}\"{symv}";
                NewFile += now + "\n";
            }
            return NewFile;
        }
        private static string SoberiStroky(List<ItemLong> list)
        {
            string NewFile = "";
            for (int i = 0; i < list.Count; i++)
            {
                string symv = @"/>";
                string newname = list[i].Name;
                string newvalue = list[i].Value.ToString();
                string now = $"<long [{newname.Length}]name=\"{newname}\" [{newvalue.Length}]value=\"{newvalue}\"{symv}";
                NewFile += now + "\n";
            }
            return NewFile;
        }
        private static string SoberiStroky(List<ItemDecimal> list)
        {
            string NewFile = "";
            for (int i = 0; i < list.Count; i++)
            {
                string symv = @"/>";
                string newname = list[i].Name;
                string newvalue = list[i].Value.ToString();
                string now = $"<decimal [{newname.Length}]name=\"{newname}\" [{newvalue.Length}]value=\"{newvalue}\"{symv}";
                NewFile += now + "\n";
            }
            return NewFile;
        }
        private static string SoberiStroky(List<ItemFloat> list)
        {
            string NewFile = "";
            for (int i = 0; i < list.Count; i++)
            {
                string symv = @"/>";
                string newname = list[i].Name;
                string newvalue = list[i].Value.ToString();
                string now = $"<float [{newname.Length}]name=\"{newname}\" [{newvalue.Length}]value=\"{newvalue}\"{symv}";
                NewFile += now + "\n";
            }
            return NewFile;
        }
        private static string SoberiStroky(List<ItemDouble> list)
        {
            string NewFile = "";
            for (int i = 0; i < list.Count; i++)
            {
                string symv = @"/>";
                string newname = list[i].Name;
                string newvalue = list[i].Value.ToString();
                string now = $"<double [{newname.Length}]name=\"{newname}\" [{newvalue.Length}]value=\"{newvalue}\"{symv}";
                NewFile += now + "\n";
            }
            return NewFile;
        }

        public static string Get(this mString ms)
        {   // 0 - name
            // 1 - async?
            // 2 - path
            // 3 - Index List Class
            // 4 - type

            string name = ms.Name;
            string path = ms.Path;
            string myType = ms.Type;
            string syncOrAsync = ms.SyncOrNot;
            int IndexClass = int.Parse(ms.IndexClass);
            CheckedResouse(path, IndexClass);
            string dex = "";
        metochka:;
            if (!SuperParams.disposable[IndexClass])
            {
                    for (int i = 0; i < SuperParams.SuperStringList[IndexClass].Count; i++)
                    {
                        if (SuperParams.SuperStringList[IndexClass][i].Name == name)
                        {
                            dex = SuperParams.SuperStringList[IndexClass][i].Value;
                            return dex;
                        }
                    }               
            }
            else
            {
                Refresh(path, IndexClass);
                goto metochka;
            }
            return null;
        }
        public static int Get(this mInt mi)
        {
     
            int dex = 0;
            string name = mi.Name;
            string path = mi.Path;
            string myType = mi.Type;
            string syncOrAsync = mi.SyncOrNot;
            int IndexClass = int.Parse(mi.IndexClass);
            CheckedResouse(path, IndexClass);
        metochka:;
            if (!SuperParams.disposable[IndexClass])
            {
                    for (int i = 0; i < SuperParams.SuperIntList[IndexClass].Count; i++)
                    {
                        if (SuperParams.SuperIntList[IndexClass][i].Name == name)
                        {
                            dex = SuperParams.SuperIntList[IndexClass][i].Value;
                            return dex;
                        }
                    }
            }
            else
            {
                Refresh(path, IndexClass);
                goto metochka;
            }
            
            return 0;
        }
        public static long Get(this mLong ml)
        {
            string name = ml.Name;
            string path = ml.Path;
            string myType = ml.Type;
            string syncOrAsync = ml.SyncOrNot;
            int IndexClass = int.Parse(ml.IndexClass);
            CheckedResouse(path, IndexClass);
        metochka:;
            if (!SuperParams.disposable[IndexClass])
            {
                    long dex = 0;
                    for (int i = 0; i < SuperParams.SuperLongList[IndexClass].Count; i++)
                    {
                        if (SuperParams.SuperLongList[IndexClass][i].Name == name)
                        {
                            dex = SuperParams.SuperLongList[IndexClass][i].Value;
                            return dex;
                        }
                    }
            }
            else
            {
                Refresh(path, IndexClass);
                goto metochka;
            }
             
            return 0; 
        }
        public static decimal Get(this mDecimal md)
        {
            string name = md.Name;
            string path = md.Path;
            string myType = md.Type;
            string syncOrAsync = md.SyncOrNot;
            int IndexClass = int.Parse(md.IndexClass);
            CheckedResouse(path, IndexClass);
        metochka:;
            if (!SuperParams.disposable[IndexClass])
            {
                    decimal dex = 0;
                    for (int i = 0; i < SuperParams.SuperDecimalList[IndexClass].Count; i++)
                    {
                        if (SuperParams.SuperDecimalList[IndexClass][i].Name == name)
                        {
                            dex = SuperParams.SuperDecimalList[IndexClass][i].Value;
                            return dex;
                        }
                    }
            }
            else
            {
                Refresh(path, IndexClass);
                goto metochka;
            }

            return 0;
        }
        public static bool Get(this mBool mb)
        {
            string name = mb.Name;
            string path = mb.Path;
            string myType = mb.Type;
            string syncOrAsync = mb.SyncOrNot;
            int IndexClass = int.Parse(mb.IndexClass);
            CheckedResouse(path, IndexClass);
        metochka:;
            if (!SuperParams.disposable[IndexClass])
            {
                bool dex = true;
                    for (int i = 0; i < SuperParams.SuperBoolList[IndexClass].Count; i++)
                    {
                        if (SuperParams.SuperBoolList[IndexClass][i].Name == name)
                        {
                            if (SuperParams.SuperBoolList[IndexClass][i].Value == true)
                            {
                                return dex;
                            }

                        }
                    }
            }
            else
            {
                Refresh(path, IndexClass);
                goto metochka;
            }
            return false;
        }
        public static float Get(this mFloat fl)
        {
            string name = fl.Name;
            string path = fl.Path;
            string myType = fl.Type;
            string syncOrAsync = fl.SyncOrNot;
            int IndexClass = int.Parse(fl.IndexClass);
            CheckedResouse(path, IndexClass);
        metochka:;
            if (!SuperParams.disposable[IndexClass])
            {
                    float dex = 0;
                    for (int i = 0; i < SuperParams.SuperFloatList[IndexClass].Count; i++)
                    {
                        if (SuperParams.SuperFloatList[IndexClass][i].Name == name)
                        {
                            dex = SuperParams.SuperFloatList[IndexClass][i].Value;
                            return dex;
                        }
                    }
            }
            else
            {
                Refresh(path, IndexClass);
                goto metochka;
            }

            return 0;
        }
        public static double Get(this mDouble md)
        {
            string name = md.Name;
            string path = md.Path;
            string myType = md.Type;
            string syncOrAsync = md.SyncOrNot;
            int IndexClass = int.Parse(md.IndexClass);
            CheckedResouse(path, IndexClass);
        metochka:;
            if (!SuperParams.disposable[IndexClass])
            {
                    double dex = 0;
                    for (int i = 0; i < SuperParams.SuperDoubleList[IndexClass].Count; i++)
                    {
                        if (SuperParams.SuperDoubleList[IndexClass][i].Name == name)
                        {
                            
                            dex = SuperParams.SuperDoubleList[IndexClass][i].Value;
                            return dex;
                        }
                    }
            }
            else
            {
                Refresh(path, IndexClass);
                goto metochka;
            }

            return 0;
        }

        public static void Delete(this mString m)
        {
            // 0 - name
            // 1 - async?
            // 2 - path
            // 3 - Index List Class
            // 4 - type
            string thisname = m.Name;
            string path = m.Path;
            string myType = m.Type;
            string syncOrAsync = m.SyncOrNot;
            int IndexClass = int.Parse(m.IndexClass);
            string NewFile = "";
            CheckedResouse(path, IndexClass);
            try
            {
                    List<ItemString> itemlist = new List<ItemString>();
                    ItemString item;
                    for (int i = 0; i< SuperParams.SuperStringList[IndexClass].Count; i++)
                    {

                        if (SuperParams.SuperStringList[IndexClass][i].Name != thisname)
                        {
                            item = new ItemString(SuperParams.SuperStringList[IndexClass][i].Name, SuperParams.SuperStringList[IndexClass][i].Value);
                            itemlist.Add(item);
                        }
                    }
                    SuperParams.SuperStringList[IndexClass] = new List<ItemString>(itemlist);

                NewFile = SuperNewFile(IndexClass);

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(NewFile);
                    sw.Close();
                }
            }
            catch { }
         
        }
        public static void Delete(this mInt m)
        {
            // 0 - name
            // 1 - async?
            // 2 - path
            // 3 - Index List Class
            // 4 - type
         
            try
            {
                string thisname = m.Name;
                string path = m.Path;
                string myType = m.Type;
                string syncOrAsync = m.SyncOrNot;
                int IndexClass = int.Parse(m.IndexClass);
                string NewFile = "";
                CheckedResouse(path, IndexClass);

                //перебрать список со строками (если он там есть конечно) 
                //если нашлось имя, удалить элемент
                //перезаписать весь файл с новыми значениями
                List<ItemInt> itemlist = new List<ItemInt>();
                ItemInt item;
                for (int i = 0; i < SuperParams.SuperIntList[IndexClass].Count; i++)
                {
              
                    if (SuperParams.SuperIntList[IndexClass][i].Name != thisname)
                    {
                        item = new ItemInt(SuperParams.SuperIntList[IndexClass][i].Name, SuperParams.SuperIntList[IndexClass][i].Value);
                        itemlist.Add(item);
                    }
                }
                SuperParams.SuperIntList[IndexClass] = new List<ItemInt>(itemlist);
              

                NewFile = SuperNewFile(IndexClass);

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(NewFile);
                    sw.Close();
                }
            }
            catch { }

        }
        public static void Delete(this mBool m)
        {
            // 0 - name
            // 1 - async?
            // 2 - path
            // 3 - Index List Class
            // 4 - type

            try
            {
                string thisname = m.Name;
                string path = m.Path;
                string myType = m.Type;
                string syncOrAsync = m.SyncOrNot;
                int IndexClass = int.Parse(m.IndexClass);
                string NewFile = "";
                CheckedResouse(path, IndexClass);
                //перебрать список со строками (если он там есть конечно) 
                //если нашлось имя, удалить элемент
                //перезаписать весь файл с новыми значениями
                List<ItemBool> itemlist = new List<ItemBool>();
                ItemBool item;
                for (int i = 0; i < SuperParams.SuperBoolList[IndexClass].Count; i++)
                {

                    if (SuperParams.SuperBoolList[IndexClass][i].Name != thisname)
                    {
                        item = new ItemBool(SuperParams.SuperBoolList[IndexClass][i].Name, SuperParams.SuperBoolList[IndexClass][i].Value);
                        itemlist.Add(item);
                    }
                }
                SuperParams.SuperBoolList[IndexClass] = new List<ItemBool>(itemlist);


                NewFile = SuperNewFile(IndexClass);

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(NewFile);
                    sw.Close();
                }
            }
            catch { }

        }
        public static void Delete(this mLong m)
        {
            // 0 - name
            // 1 - async?
            // 2 - path
            // 3 - Index List Class
            // 4 - type

            try
            {
                string thisname = m.Name;
                string path = m.Path;
                string myType = m.Type;
                string syncOrAsync = m.SyncOrNot;
                int IndexClass = int.Parse(m.IndexClass);
                string NewFile = "";
                CheckedResouse(path, IndexClass);
                //перебрать список со строками (если он там есть конечно) 
                //если нашлось имя, удалить элемент
                //перезаписать весь файл с новыми значениями
                List<ItemLong> itemlist = new List<ItemLong>();
                ItemLong item;
                for (int i = 0; i < SuperParams.SuperLongList[IndexClass].Count; i++)
                {

                    if (SuperParams.SuperLongList[IndexClass][i].Name != thisname)
                    {
                        item = new ItemLong(SuperParams.SuperLongList[IndexClass][i].Name, SuperParams.SuperLongList[IndexClass][i].Value);
                        itemlist.Add(item);
                    }
                }
                SuperParams.SuperLongList[IndexClass] = new List<ItemLong>(itemlist);


                NewFile = SuperNewFile(IndexClass);

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(NewFile);
                    sw.Close();
                }
            }
            catch { }

        }
        public static void Delete(this mDecimal m)
        {
            // 0 - name
            // 1 - async?
            // 2 - path
            // 3 - Index List Class
            // 4 - type

            try
            {
                string thisname = m.Name;
                string path = m.Path;
                string myType = m.Type;
                string syncOrAsync = m.SyncOrNot;
                int IndexClass = int.Parse(m.IndexClass);
                string NewFile = "";
                CheckedResouse(path, IndexClass);
                //перебрать список со строками (если он там есть конечно) 
                //если нашлось имя, удалить элемент
                //перезаписать весь файл с новыми значениями
                List<ItemDecimal> itemlist = new List<ItemDecimal>();
                ItemDecimal item;
                for (int i = 0; i < SuperParams.SuperDecimalList[IndexClass].Count; i++)
                {

                    if (SuperParams.SuperDecimalList[IndexClass][i].Name != thisname)
                    {
                        item = new ItemDecimal(SuperParams.SuperDecimalList[IndexClass][i].Name, SuperParams.SuperDecimalList[IndexClass][i].Value);
                        itemlist.Add(item);
                    }
                }
                SuperParams.SuperDecimalList[IndexClass] = new List<ItemDecimal>(itemlist);


                NewFile = SuperNewFile(IndexClass);

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(NewFile);
                    sw.Close();
                }
            }
            catch { }

        }
        public static void Delete(this mFloat m)
        {
            // 0 - name
            // 1 - async?
            // 2 - path
            // 3 - Index List Class
            // 4 - type

            try
            {
                string thisname = m.Name;
                string path = m.Path;
                string myType = m.Type;
                string syncOrAsync = m.SyncOrNot;
                int IndexClass = int.Parse(m.IndexClass);
                string NewFile = "";
                CheckedResouse(path, IndexClass);
                //перебрать список со строками (если он там есть конечно) 
                //если нашлось имя, удалить элемент
                //перезаписать весь файл с новыми значениями
                List<ItemFloat> itemlist = new List<ItemFloat>();
                ItemFloat item;
                for (int i = 0; i < SuperParams.SuperFloatList[IndexClass].Count; i++)
                {

                    if (SuperParams.SuperFloatList[IndexClass][i].Name != thisname)
                    {
                        item = new ItemFloat(SuperParams.SuperFloatList[IndexClass][i].Name, SuperParams.SuperFloatList[IndexClass][i].Value);
                        itemlist.Add(item);
                    }
                }
                SuperParams.SuperFloatList[IndexClass] = new List<ItemFloat>(itemlist);


                NewFile = SuperNewFile(IndexClass);

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(NewFile);
                    sw.Close();
                }
            }
            catch { }

        }
        public static void Delete(this mDouble m)
        {
            // 0 - name
            // 1 - async?
            // 2 - path
            // 3 - Index List Class
            // 4 - type

            try
            {
                string thisname = m.Name;
                string path = m.Path;
                string myType = m.Type;
                string syncOrAsync = m.SyncOrNot;
                int IndexClass = int.Parse(m.IndexClass);
                string NewFile = "";
                CheckedResouse(path, IndexClass);
                //перебрать список со строками (если он там есть конечно) 
                //если нашлось имя, удалить элемент
                //перезаписать весь файл с новыми значениями
                List<ItemDouble> itemlist = new List<ItemDouble>();
                ItemDouble item;
                for (int i = 0; i < SuperParams.SuperFloatList[IndexClass].Count; i++)
                {

                    if (SuperParams.SuperDoubleList[IndexClass][i].Name != thisname)
                    {
                        item = new ItemDouble(SuperParams.SuperDoubleList[IndexClass][i].Name, SuperParams.SuperDoubleList[IndexClass][i].Value);
                        itemlist.Add(item);
                    }
                }
                SuperParams.SuperDoubleList[IndexClass] = new List<ItemDouble>(itemlist);


                NewFile = SuperNewFile(IndexClass);

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(NewFile);
                    sw.Close();
                }
            }
            catch { }

        }


        private static void CheckedResouse(string path, int IndexClass)
        {
            //если ресурсы очищены
            if (SuperParams.disposable[IndexClass])
            {
                Refresh(path, IndexClass);
            }
        }
        private static string SuperNewFile(int IndexClass)
        {
            string NewFile = "";
            NewFile += SoberiStroky(SuperParams.SuperBoolList[IndexClass]);
            NewFile += SoberiStroky(SuperParams.SuperStringList[IndexClass]);
            NewFile += SoberiStroky(SuperParams.SuperIntList[IndexClass]);
            NewFile += SoberiStroky(SuperParams.SuperLongList[IndexClass]);
            NewFile += SoberiStroky(SuperParams.SuperDecimalList[IndexClass]);
            NewFile += SoberiStroky(SuperParams.SuperFloatList[IndexClass]);
            NewFile += SoberiStroky(SuperParams.SuperDoubleList[IndexClass]);
            return NewFile;
        }
        private static void Packing(string text, int IndexClass)
        {
            /*
              здесь все данные фасуются по листам 
                с соответствующим типом даннных

                <string [3]name="abc" [5]value="asdfg"/>
                <int [3]name="abc" [5]value="12345"/>
            */
            //string name = "string";
            string[] mytypes = { "int", "string", "bool", "long", "decimal", "float", "double" };
            int typescr = 0;
            string type = "";
            string originaltype = "";
            string buffer = "";
            int buf = 0;
            string newtext = "";

            string mtName = "";
            string mtValue = "";

            int textlength = text.Length;

            for (int i = 0; i < textlength; i++)
            {
                try
                {

                    if (text[i] == '<')
                    {
                        i++;
                    metka:;
                        string dantype = mytypes[typescr];
                        int dantypelength = dantype.Length;
                        type = text.Substring(i, dantypelength);
                        if (type == dantype)
                        {
                            originaltype = dantype;
                        }
                        else
                        {
                            typescr++;
                            goto metka;
                        }

                        // <string [10]name="abcdefghij" [5]value="asdfg"/>
                        i += dantypelength + 1;
                    metka2:;
                        for (int I = i; I < textlength; I++)
                        {
                            if (text[I] == '[')
                            {
                                I++;
                                i = I;
                                newtext = text.Substring(I);
                                int where = newtext.IndexOf(']');
                                buffer = newtext.Substring(0, where);
                                buf = int.Parse(buffer);
                                i += buffer.Length + 1;
                                I = i;
                            }
                            string mName = "name";
                            string mValue = "value";
                            int mNamelength = mName.Length;
                            int mValuelength = mValue.Length;

                            if (mtName == "" || mtValue == "")
                            {
                                if (text.Substring(I, mNamelength) == mName)
                                {
                                    I += mNamelength + 2;
                                    i = I;
                                    mtName = text.Substring(I, buf);
                                    I += mNamelength + 1;
                                    i = I;
                                    goto metka2;



                                }
                                if (text.Substring(I, mValuelength) == mValue)
                                {
                                    I += mValuelength + 2;
                                    i = I;
                                    mtValue = text.Substring(I, buf);
                                    I += mValuelength + 1;
                                    i = I;
                                    goto metka2;


                                }
                            }
                            else
                            {
                                break;
                            }


                        }

                        if (originaltype == "int")
                        {
                            ItemInt item = new ItemInt(mtName, int.Parse(mtValue));
                            SuperParams.SuperIntList[IndexClass].Add(item);

                        }
                        if (originaltype == "string")
                        {
                            ItemString item = new ItemString(mtName, mtValue);
                            SuperParams.SuperStringList[IndexClass].Add(item);

                        }
                        if (originaltype == "bool")
                        {
                            bool my = false;
                            if (mtValue == "true")
                            {
                                my = true;
                            }
                            ItemBool item = new ItemBool(mtName, my);
                            SuperParams.SuperBoolList[IndexClass].Add(item);
                        }
                        if (originaltype == "long")
                        {
                            ItemLong item = new ItemLong(mtName, long.Parse(mtValue));
                            SuperParams.SuperLongList[IndexClass].Add(item);
                        }
                        if (originaltype == "decimal")
                        {
                            ItemDecimal item = new ItemDecimal(mtName, decimal.Parse(mtValue));
                            SuperParams.SuperDecimalList[IndexClass].Add(item);
                        }
                        if (originaltype == "float")
                        {
                            ItemFloat item = new ItemFloat(mtName, float.Parse(mtValue));
                            SuperParams.SuperFloatList[IndexClass].Add(item);
                        }
                        if (originaltype == "double")
                        {
                            ItemDouble item = new ItemDouble(mtName, double.Parse(mtValue));
                            SuperParams.SuperDoubleList[IndexClass].Add(item);
                        }

                        mtName = "";
                        mtValue = "";
                        originaltype = "";
                        typescr = 0;



                    }
                }
                catch { }


            }
            SuperParams.disposable[IndexClass] = false;

        }
        private static string ReadFile(string path)
        {
            string File = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    File = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch { }
            return File;
        }
        private static void Refresh(string path, int IndexClass)
        {
            string text = "";

            if (!File.Exists(path))
            {
                try
                {
                    File.Create(path);
                }
                catch (Exception e) { Console.WriteLine(e.Message); }

            }
            //записть всего файла в строку
            try
            {
                text = ReadFile(path);
                Packing(text, IndexClass);

                text = "";

            }
            catch { }
        }

    }
}
