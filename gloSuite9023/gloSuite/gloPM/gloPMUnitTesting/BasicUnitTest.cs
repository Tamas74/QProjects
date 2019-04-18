using System;
using System.Collections.Generic;
using System.Text;

namespace gloPMUnitTesting
{
     class BasicUnitTest
    {
         // 20101901 Mahesh Nawal Create the class for Dynamic testing of the function.
         public static  string[] Parameterstring ={" ","Null","ABC","abc","abc","abc~!@#$%^&*","abc'","abc/.[]{}|"};
         public static  int[] Parameterint ={ 0000, -123, 1243};
         public static int[] Parameterint64 ={ 0000, -123, 1243 };
         public static long[] Parameterlong ={ 0000, -123, 1243};
         public static string[] Parameterbool ={"true","false"}; 
         public static string Databasestring = "Data Source=GLOINT1;Database=gloPM2010_Production;User ID=sa;Password=sagloint1";
         

         public System.Reflection.ParameterInfo[] ListProperties(object objectToInspect, string methodname)
         {
             string returnString = "";

             Type objectType = objectToInspect.GetType();
             //  objectType.DeclaringMethod.Name;
             System.Reflection.MethodInfo[] methods =
             objectType.GetMethods();

             foreach (System.Reflection.MethodInfo method in methods)
             {

                 //   System.Reflection.MethodBody mb = method.GetMethodBody();

                 if (method.Name.ToString() == methodname)
                 {
                     System.Reflection.ParameterInfo[] parameter = method.GetParameters();
                     //returnString += method.GetParameter;
                     return parameter;
                     
                 }            
             }
             return null;
         }
         public System.Reflection.MethodInfo MethodInfo(object objectToInspect, string methodname)
         {
             string returnString = "";
             Type objectType = objectToInspect.GetType();
            
             System.Reflection.MethodInfo[] methods =
             objectType.GetMethods();

             foreach (System.Reflection.MethodInfo method in methods)
             {
                 //   System.Reflection.MethodBody mb = method.GetMethodBody();
                 if (method.Name.ToString() == methodname)
                 {
                     return method;
                 }                 
             }
             return null;
         }

         public bool TesttheFunction(object classobject, string functionname, bool expected)
         {
             bool actual=true;
             bool flag=true;
             System.Reflection.ParameterInfo[] parameterdata = ListProperties(classobject, functionname);
             System.Reflection.MethodInfo method = MethodInfo(classobject, functionname);
             System.Collections.ArrayList paralist = new System.Collections.ArrayList();
             paralist = ParameterArray(parameterdata);

             System.IO.StreamWriter Tex;
             string strLogFilePath = "D:\\TestCaseReport\\";
             if (System.IO.Directory.Exists(strLogFilePath) == false)
             {
                 System.IO.Directory.CreateDirectory(strLogFilePath);
             }
             string strLogFileName;
             strLogFileName = System.DateTime.Now.ToString("yyyy") + System.DateTime.Now.ToString("MM") + System.DateTime.Now.ToString("dd");
             strLogFileName = "TestCaseReportof" + method.Name.ToString() + strLogFileName + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second;
             strLogFilePath = strLogFilePath + strLogFileName + ".csv";
             System.IO.FileInfo t = new System.IO.FileInfo(strLogFilePath);
             Tex = t.CreateText();
             string heading = "";
             string details = "";
             for (int z = 0; z < parameterdata.GetLength(0); z++)
             {
                 heading = heading + parameterdata[z].Name + " , ";
             }
             heading = "No.,"+heading + " Result";

             Tex.WriteLine(heading);

             for (int i = 0; i < paralist.Count; i++)
             {
                 
                 string parameter = paralist[i].ToString().Remove(paralist[i].ToString().Length - 1);
                 string[] values;
                 values = parameter.Split(",".ToCharArray());                 
               //  long DeptID = Convert.ToInt64(values[0]);
                // string DepartmentName = values[1].ToString();
                Object[] arg = new Object [parameterdata.GetLength(0)];
                
                     for(int y=0;y<parameterdata.GetLength(0);y++)
                     {
                         if (parameterdata[y].ParameterType.Name == "Int")
                         {
                             arg[y] = Convert.ToInt64(values[y]);
                         }
                         if (parameterdata[y].ParameterType.Name == "Int64")
                         {
                             arg[y] = Convert.ToInt64(values[y]);
                         }
                         if (parameterdata[y].ParameterType.Name == "Int32")
                         {
                             arg[y] = Convert.ToInt32(values[y]);
                         }
                         if (parameterdata[y].ParameterType.Name == "Int16")
                         {
                             arg[y] = Convert.ToInt16(values[y]);
                         }
                         if (parameterdata[y].ParameterType.Name == "String")
                         {
                             arg[y] = values[y].ToString();
                         }
                         if (parameterdata[y].ParameterType.Name == "Boolean")
                         {
                             arg[y] = Convert.ToBoolean(values[y].ToString());
                         }
                         details = details + values[y].ToString() + ",";
                     }
                     try
                     {
                         actual = Convert.ToBoolean(method.Invoke(classobject, arg));
                         if (actual == expected)
                         {
                             Tex.WriteLine(i.ToString() + "," + details + " Successfully Pass");
                         }
                         else
                         {
                             flag = false;
                             Tex.WriteLine(i.ToString() + "," + details + " Fail");
                             // System.Windows.Forms.MessageBox.Show("Tested with this parameter :- Departmen ID ->" + DeptID + "DepartmentName ->" + DepartmentName);
                         }
                     }
                     catch (Exception e)
                     {
                         flag = false;
                         Tex.WriteLine(i.ToString() + "," + details + " Fail");

                     }
                         details = "";
             }
             Tex.Close();

             return flag;
             }

         public bool TesttheFunction(object classobject, string functionname, string expected)
         {
             string  actual = "";
             bool flag = true;
             System.Reflection.ParameterInfo[] parameterdata = ListProperties(classobject, functionname);
             System.Reflection.MethodInfo method = MethodInfo(classobject, functionname);
             System.Collections.ArrayList paralist = new System.Collections.ArrayList();
             paralist = ParameterArray(parameterdata);

             System.IO.StreamWriter Tex;
             string strLogFilePath = "D:\\TestCaseReport\\";
             string strLogFileName;
             strLogFileName = System.DateTime.Now.ToString("yyyy") + System.DateTime.Now.ToString("MM") + System.DateTime.Now.ToString("dd");
             strLogFileName = "TestCaseReportof" + method.Name.ToString() + strLogFileName + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second;
             strLogFilePath = strLogFilePath + strLogFileName + ".csv";
             System.IO.FileInfo t = new System.IO.FileInfo(strLogFilePath);
             Tex = t.CreateText();
             string heading = "";
             string details = "";
             for (int z = 0; z < parameterdata.GetLength(0); z++)
             {
                 heading = heading + parameterdata[z].Name + " , ";
             }
             heading = "No.," + heading + " Result";

             Tex.WriteLine(heading);


             for (int i = 0; i < paralist.Count; i++)
             {

                 string parameter = paralist[i].ToString().Remove(paralist[i].ToString().Length - 1);
                 string[] values;
                 values = parameter.Split(",".ToCharArray());

                 //  long DeptID = Convert.ToInt64(values[0]);

                 // string DepartmentName = values[1].ToString();
                 Object[] arg = new Object[parameterdata.GetLength(0)];

                 for (int y = 0; y < parameterdata.GetLength(0); y++)
                 {
                     if (parameterdata[y].ParameterType.Name == "Int")
                     {
                         arg[y] = Convert.ToInt64(values[y]);
                     }
                     if (parameterdata[y].ParameterType.Name == "Int64")
                     {
                         arg[y] = Convert.ToInt64(values[y]);
                     }
                     if (parameterdata[y].ParameterType.Name == "Int32")
                     {
                         arg[y] = Convert.ToInt32(values[y]);
                     }
                     if (parameterdata[y].ParameterType.Name == "Int16")
                     {
                         arg[y] = Convert.ToInt16(values[y]);
                     }
                     if (parameterdata[y].ParameterType.Name == "String")
                     {
                         arg[y] = values[y].ToString();
                     }
                     if (parameterdata[y].ParameterType.Name == "Boolean")
                     {
                         arg[y] = Convert.ToBoolean(values[y].ToString());
                     }
                     details = details + values[y].ToString() + ",";
                 }
                 try
                 {
                     actual = Convert.ToString(method.Invoke(classobject, arg));
                     if (actual != expected)
                     {
                         Tex.WriteLine(i.ToString() +","+details + " Successfully Pass");
                     }
                     else
                     {
                         flag = false;
                         Tex.WriteLine(i.ToString() +"," +details + " Fail");
                         // System.Windows.Forms.MessageBox.Show("Tested with this parameter :- Departmen ID ->" + DeptID + "DepartmentName ->" + DepartmentName);
                     }
                 }
                 catch (Exception e)
                 {
                     flag = false;
                     Tex.WriteLine(i.ToString() + "," + details + " Fail");

                 }
                 details = "";
             }
             Tex.Close();
             //actual = "classobject.functionname(DeptID, DepartmentName)";
             return flag;
           
         }
         public bool  TesttheFunction(object classobject, string functionname, Int64  expected)
         {
             

             Int64  actual = 0 ;
             bool flag = true;
             System.Reflection.ParameterInfo[] parameterdata = ListProperties(classobject, functionname);
             System.Reflection.MethodInfo method = MethodInfo(classobject, functionname);
             System.Collections.ArrayList paralist = new System.Collections.ArrayList();
             paralist = ParameterArray(parameterdata);

             System.IO.StreamWriter Tex;
             string strLogFilePath = "D:\\TestCaseReport\\";
             string strLogFileName;
             strLogFileName = System.DateTime.Now.ToString("yyyy") + System.DateTime.Now.ToString("MM") + System.DateTime.Now.ToString("dd");
             strLogFileName = "TestCaseReportof" + method.Name.ToString() + strLogFileName + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second;
             strLogFilePath = strLogFilePath + strLogFileName + ".csv";
             System.IO.FileInfo t = new System.IO.FileInfo(strLogFilePath);
             Tex = t.CreateText();
             string heading="";
             string details="";
             for(int z=0;z<parameterdata.GetLength(0); z++)
             {
                 heading = heading + parameterdata[z].Name + " , ";                  
             }
             heading = "No.," + heading + " Result";
             
             Tex.WriteLine(heading);

             for (int i = 0; i < paralist.Count; i++)
             {

                 string parameter = paralist[i].ToString().Remove(paralist[i].ToString().Length - 1);
                 string[] values;
                 values = parameter.Split(",".ToCharArray());

                 //  long DeptID = Convert.ToInt64(values[0]);

                 // string DepartmentName = values[1].ToString();
                 Object[] arg = new Object[parameterdata.GetLength(0)];

                 for (int y = 0; y < parameterdata.GetLength(0); y++)
                 {
                     if (parameterdata[y].ParameterType.Name == "Int")
                     {
                         arg[y] = Convert.ToInt64(values[y]);
                     }
                     if (parameterdata[y].ParameterType.Name == "Int64")
                     {
                         arg[y] = Convert.ToInt64(values[y]);
                     }
                     if (parameterdata[y].ParameterType.Name == "Int32")
                     {
                         arg[y] = Convert.ToInt32(values[y]);
                     }
                     if (parameterdata[y].ParameterType.Name == "Int16")
                     {
                         arg[y] = Convert.ToInt16(values[y]);
                     }
                     if (parameterdata[y].ParameterType.Name == "String")
                     {
                         arg[y] = values[y].ToString();
                     }
                     if (parameterdata[y].ParameterType.Name == "Boolean")
                     {
                         arg[y] = Convert.ToBoolean(values[y].ToString());
                     }
                     details=details+values[y].ToString() +",";
                 }
                 try
                 {
                     actual = Convert.ToInt64(method.Invoke(classobject, arg));

                     if (actual != expected)
                     {
                         Tex.WriteLine(i.ToString() + "," +details + " Successfully Pass");
                     }
                     else
                     {
                         flag = false;
                         Tex.WriteLine(i.ToString() + "," + details + " Fail");
                         // System.Windows.Forms.MessageBox.Show("Tested with this parameter :- Departmen ID ->" + DeptID + "DepartmentName ->" + DepartmentName);
                     }
                 }
                 catch (Exception e)
                 {
                     flag = false;
                     Tex.WriteLine(i.ToString() +","+details + " Fail");
                 
                 }
              
                 details = "";
             }
             Tex.Close();
             return flag;
         }



         //public object GetClassObject(string namespacestring,string className)
         //{
         //    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
         //    System.Reflection.AssemblyName assemblyName = assembly.GetName();
         //    Type t = assembly.GetType(assemblyName.Name + "." + className);
         //    return (namespacestring)Activator.CreateInstance(t);
         //}


         public System.Collections.ArrayList ParameterArray(System.Reflection.ParameterInfo[] parameterdata)
         {
             System.Collections.ArrayList paralist = new System.Collections.ArrayList();
             //   int count=parameterdata.;
             for (int i = 0; i < parameterdata.GetLength(0); i++)
             {
                 if (parameterdata[i].ParameterType.Name == "Int")
                 {
                     paralist.Add("0");
                 }
                 if (parameterdata[i].ParameterType.Name == "Int64")
                 {
                     paralist.Add("1");
                 }
                 if (parameterdata[i].ParameterType.Name == "Int32")
                 {
                     paralist.Add("1");
                 }
                 if (parameterdata[i].ParameterType.Name == "Int16")
                 {
                     paralist.Add("1");
                 }
                 if (parameterdata[i].ParameterType.Name == "String")
                 {
                     paralist.Add("2");
                 }
                 if (parameterdata[i].ParameterType.Name == "long")
                 {
                     paralist.Add("3");
                 }
                 if (parameterdata[i].ParameterType.Name == "Boolean")
                 {
                     paralist.Add("4");
                 }

             }
             System.Collections.ArrayList data = new System.Collections.ArrayList();
           //  System.Collections.ArrayList tempdata = new System.Collections.ArrayList();
             string[] tempdata=new string[2000];
             int dataindex = 0;
             bool flag = true;
             for (int z = 0; z < paralist.Count; z++)
             {
                 //tempdata = data.;

                 data.CopyTo(tempdata);

                 if (paralist[z].ToString() == "1")
                 {
                     int count = data.Count;
                     //   int =BasicUnitTest.Parameterstring.GetLength(0);
                     if (count == 0)
                     {
                         for (int arrydata = 0; arrydata < BasicUnitTest.Parameterint64.GetLength(0); arrydata++)
                         {
                             data.Insert(arrydata, BasicUnitTest.Parameterint64[arrydata].ToString() + ",");
                         }
                     }
                     else
                     {
                         for (int arrydata = 0; arrydata < BasicUnitTest.Parameterint64.GetLength(0); arrydata++)
                         {
                             for (int arraycount = 0; arraycount < count; arraycount++)
                             {
                                 if (arrydata == 0)
                                 {
                                     if (count != 0)
                                     {
                                         string temp = data[arraycount].ToString();
                                         data.RemoveAt(arraycount);
                                         data.Insert(arraycount, temp + BasicUnitTest.Parameterint64[arrydata].ToString() + ",");
                                     }
                                     else
                                     {
                                         data.Insert(arrydata, BasicUnitTest.Parameterint64[arrydata].ToString() + ",");
                                     }
                                 }
                                 else
                                 {
                                     flag = false;
                                     data.Insert(dataindex, tempdata[arraycount].ToString() + BasicUnitTest.Parameterint64[arrydata].ToString() + ",");
                                     dataindex = dataindex + 1;
                                 }
                                 if (flag == true)
                                 {
                                     dataindex = arraycount + 1;
                                 }
                             }

                         }
                     }
                 }
                 if (paralist[z].ToString() == "2")
                 {
                     int count = data.Count;
                     //   int =BasicUnitTest.Parameterstring.GetLength(0);
                     if (count == 0)
                     {
                         for (int arrydata = 0; arrydata < BasicUnitTest.Parameterstring.GetLength(0); arrydata++)
                         {
                             data.Insert(arrydata, BasicUnitTest.Parameterstring[arrydata].ToString() + ",");
                         }
                     }
                     else
                     {
                         for (int arrydata = 0; arrydata < BasicUnitTest.Parameterstring.GetLength(0); arrydata++)
                         {
                             for (int arraycount = 0; arraycount < count; arraycount++)
                             {
                                 if (arrydata == 0)
                                 {
                                     if (count != 0)
                                     {
                                         string temp = data[arraycount].ToString();
                                         data.RemoveAt(arraycount);
                                         data.Insert(arraycount, temp + BasicUnitTest.Parameterstring[arrydata].ToString() + ",");
                                     }
                                     else
                                     {
                                         data.Insert(arrydata, BasicUnitTest.Parameterstring[arrydata].ToString() + ",");
                                     }
                                 }
                                 else
                                 {
                                     flag = false;
                                     // data[dataindex] = data[arraycount] + BasicUnitTest.Parameterstring[arraycount].ToString();
                                     data.Insert(dataindex, tempdata[arraycount].ToString() + BasicUnitTest.Parameterstring[arrydata].ToString() + ",");
                                     dataindex = dataindex + 1;
                                 }
                                 if (flag == true)
                                 {
                                     dataindex = arraycount + 1;
                                 }
                             }

                         }
                     }
                 }


                 if (paralist[z].ToString() == "0")
                 {
                     int count = data.Count;
                     //   int =BasicUnitTest.Parameterstring.GetLength(0);
                     if (count == 0)
                     {
                         for (int arrydata = 0; arrydata < BasicUnitTest.Parameterint.GetLength(0); arrydata++)
                         {
                             data.Insert(arrydata, BasicUnitTest.Parameterint[arrydata].ToString() + ",");
                         }
                     }
                     else
                     {
                         for (int arrydata = 0; arrydata < BasicUnitTest.Parameterint.GetLength(0); arrydata++)
                         {
                             for (int arraycount = 0; arraycount < count; arraycount++)
                             {
                                 if (arrydata == 0)
                                 {
                                     if (count != 0)
                                     {
                                         string temp = data[arraycount].ToString();
                                         data.RemoveAt(arraycount);
                                         data.Insert(arraycount, temp + BasicUnitTest.Parameterint[arrydata].ToString() + ",");
                                     }
                                     else
                                     {
                                         data.Insert(arrydata, BasicUnitTest.Parameterint[arrydata].ToString() + ",");
                                     }
                                 }
                                 else
                                 {
                                     flag = false;
                                     data.Insert(dataindex, tempdata[arraycount].ToString() + BasicUnitTest.Parameterint[arrydata].ToString() + ",");
                                     dataindex = dataindex + 1;
                                 }
                                 if (flag == true)
                                 {
                                     dataindex = arraycount + 1;
                                 }
                             }

                         }
                     }
                 }

                 if (paralist[z].ToString() == "3")
                 {
                     int count = data.Count;
                     //   int =BasicUnitTest.Parameterstring.GetLength(0);
                     if (count == 0)
                     {
                         for (int arrydata = 0; arrydata < BasicUnitTest.Parameterlong.GetLength(0); arrydata++)
                         {
                             data.Insert(arrydata, BasicUnitTest.Parameterlong[arrydata].ToString() + ",");
                         }
                     }
                     else
                     {
                         for (int arrydata = 0; arrydata < BasicUnitTest.Parameterlong.GetLength(0); arrydata++)
                         {
                             for (int arraycount = 0; arraycount < count; arraycount++)
                             {
                                 if (arrydata == 0)
                                 {
                                     if (count != 0)
                                     {
                                         string temp = data[arraycount].ToString();
                                         data.RemoveAt(arraycount);
                                         data.Insert(arraycount, temp + BasicUnitTest.Parameterlong[arrydata].ToString() + ",");
                                     }
                                     else
                                     {
                                         data.Insert(arrydata, BasicUnitTest.Parameterlong[arrydata].ToString() + ",");
                                     }
                                 }
                                 else
                                 {
                                     flag = false;
                                     data.Insert(dataindex, tempdata[arraycount].ToString() + BasicUnitTest.Parameterlong[arrydata].ToString() + ",");
                                     dataindex = dataindex + 1;
                                 }
                                 if (flag == true)
                                 {
                                     dataindex = arraycount + 1;
                                 }
                             }

                         }
                     }
                 }
                 if (paralist[z].ToString() == "4")
                 {
                     int count = data.Count;
                     //   int =BasicUnitTest.Parameterstring.GetLength(0);
                     if (count == 0)
                     {
                         for (int arrydata = 0; arrydata < BasicUnitTest.Parameterbool.GetLength(0); arrydata++)
                         {
                             data.Insert(arrydata, BasicUnitTest.Parameterbool[arrydata].ToString() + ",");
                         }
                     }
                     else
                     {
                         for (int arrydata = 0; arrydata < BasicUnitTest.Parameterbool.GetLength(0); arrydata++)
                         {
                             for (int arraycount = 0; arraycount < count; arraycount++)
                             {
                                 if (arrydata == 0)
                                 {
                                     if (count != 0)
                                     {
                                         string temp = data[arraycount].ToString();
                                         data.RemoveAt(arraycount);
                                         data.Insert(arraycount, temp + BasicUnitTest.Parameterbool[arrydata].ToString() + ",");
                                     }
                                     else
                                     {
                                         data.Insert(arrydata, BasicUnitTest.Parameterbool[arrydata].ToString() + ",");
                                     }
                                 }
                                 else
                                 {
                                     flag = false;
                                     data.Insert(dataindex, tempdata[arraycount].ToString() + BasicUnitTest.Parameterbool[arrydata].ToString() + ",");
                                     dataindex = dataindex + 1;
                                 }
                                 if (flag == true)
                                 {
                                     dataindex = arraycount + 1;
                                 }
                             }

                         }
                     }
                 }
               
             }
             return data;
         }
         
    }

}

//         Assembly a = Assembly.LoadFrom(“c:\\mymath.dll”);
//Type t = a.GetType (“mymath”);
//MethodInfo m = t.GetMethod(“add”);
//Object obj = Activator.CreateInstance (t);
//Object[ ] arg = new Object [2];
//arg[0] = 10;
//arg[1] = 20;
//m.Invoke(obj, arg);

