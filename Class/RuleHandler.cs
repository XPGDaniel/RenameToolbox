﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RenameMyAss.Class
{
    public class RuleHandler
    {
        #region Filename Handling
        public static List<FileToRename> RemoveFileName(List<FileToRename> list, string mode, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && list != null)
            {
                string filename = "", extension = "";
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName);
                    switch (mode.ToLowerInvariant())
                    {
                        case "all":
                            list[i].FileName = extension;
                            break;
                        case "left":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (filename.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(filename.Length);
                                }
                                list[i].FileName = filename.Substring(0+Convert.ToInt32(subparameter)) + extension;
                            }
                            break;
                        case "right":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (filename.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(filename.Length);
                                }
                                list[i].FileName = filename.Substring(0, filename.Length - Convert.ToInt32(subparameter)) + extension;
                            }
                            break;
                        case "center":
                            if (!string.IsNullOrEmpty(subparameter) && filename.Length >= 3)
                            {
                                int center = Convert.ToInt32(Math.Ceiling((Convert.ToDouble(filename.Length) / 2))), leftlength =0, rightlength=0;
                                if (filename.Length < (Convert.ToInt32(subparameter)*2))
                                {
                                    subparameter = Convert.ToString(center);
                                }
                                if (center + Convert.ToInt32(subparameter) > filename.Length)
                                {
                                    rightlength = filename.Length - center;
                                }
                                else
                                {
                                    rightlength = Convert.ToInt32(subparameter);
                                }
                                if (center - Convert.ToInt32(subparameter)<0)
                                {
                                    leftlength = 0;
                                }
                                else
                                {
                                    leftlength = Convert.ToInt32(subparameter);
                                }
                                list[i].FileName = filename.Substring(0, center - leftlength - 1) + filename.Substring(center + rightlength) + extension;
                            }
                            break;
                        case "firstletter":
                            if (filename.Length > 1)
                            {
                                list[i].FileName = filename.Substring(1) + extension;
                            }
                            break;
                        case "matchphase":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                list = ReplaceFileName(list, subparameter, "");
                            }
                            break;
                        case "(*)":
                            string regex = "(\\(.*\\))"; //"(\\[.*\\])|(\".*\")|('.*')|(\\(.*\\))";
                            list[i].FileName = Regex.Replace(filename, regex, "") + extension;
                            break;
                        case "(*":
                            int LeftBucketIndex = filename.IndexOf('(');
                            if (LeftBucketIndex > 0)
                            {
                                list[i].FileName = filename.Substring(0, LeftBucketIndex) + extension;
                            }
                            break;
                        case "*)":
                            int RightBucketIndex = filename.LastIndexOf(')') + 1;
                            if (RightBucketIndex > 0)
                            {
                                list[i].FileName = filename.Substring(RightBucketIndex) + extension;
                            }
                            break;
                    }
                }
            }
            return list;
        }
        public static List<FileToRename> ReplaceFileName(List<FileToRename> list, string target, string newstring)
        {
            if (!string.IsNullOrEmpty(target) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = list[i].FileName.Replace(target, newstring);
                }
            }
            return list;
        }
        public static List<FileToRename> PrefixFileName(List<FileToRename> list, string prefix)
        {
            if (!string.IsNullOrEmpty(prefix) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = prefix + list[i].FileName;
                }
            }
            return list;
        }
        public static List<FileToRename> SuffixFileName(List<FileToRename> list, string suffix)
        {
            if (!string.IsNullOrEmpty(suffix) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = Path.GetFileNameWithoutExtension(list[i].FileName) + suffix + Path.GetExtension(list[i].FileName);
                }
            }
            return list;
        }
        public static List<FileToRename> ChangeFileNameCase(List<FileToRename> list, string UpDown, string mode, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && !string.IsNullOrEmpty(UpDown) && list != null)
            {
                UpDown = UpDown.ToLowerInvariant();
                string filename = "", extension = "";
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName);
                    switch (mode.ToLowerInvariant())
                    {
                        case "all":
                            if (UpDown == "up")
                            {
                                list[i].FileName = filename.ToUpperInvariant() + extension;
                            }
                            else
                            {
                                list[i].FileName = filename.ToLowerInvariant() + extension;
                            }
                            break;
                        case "left":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (filename.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(filename.Length);
                                }
                                string ChangedLeftSlice = "";
                                if (UpDown == "up")
                                {
                                    ChangedLeftSlice = filename.Substring(0, Convert.ToInt32(subparameter)).ToUpperInvariant();
                                }
                                else
                                {
                                    ChangedLeftSlice = filename.Substring(0, Convert.ToInt32(subparameter)).ToLowerInvariant();
                                }
                                list[i].FileName = ChangedLeftSlice + filename.Substring(Convert.ToInt32(subparameter)) + extension;
                            }
                            break;
                        case "right":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (filename.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(filename.Length);
                                }
                                string ChangedRightSlice = "";
                                if (UpDown == "up")
                                {
                                    ChangedRightSlice = filename.Substring(filename.Length - Convert.ToInt32(subparameter)).ToUpperInvariant();
                                }
                                else
                                {
                                    ChangedRightSlice = filename.Substring(filename.Length - Convert.ToInt32(subparameter)).ToLowerInvariant();
                                }
                                list[i].FileName = filename.Substring(0, filename.Length - Convert.ToInt32(subparameter)) + ChangedRightSlice + extension;
                            }
                            break;
                        case "firstletter":
                            if (filename.Length > 1)
                            {
                                if (UpDown == "up")
                                {
                                    list[i].FileName = char.ToUpper(filename[0]) + filename.Substring(1) + extension;
                                }
                                else
                                {
                                    list[i].FileName = char.ToLower(filename[0]) + filename.Substring(1) + extension;
                                }
                            }
                            break;
                        case "matchphase":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (UpDown == "up")
                                {
                                    list = ReplaceFileName(list, subparameter, subparameter.ToUpperInvariant());
                                }
                                else
                                {
                                    list = ReplaceFileName(list, subparameter, subparameter.ToLowerInvariant());
                                }
                            }
                            break;
                    }
                }
            }
            return list;
        }
        public static List<FileToRename> InsertIntoFileName(List<FileToRename> list, string mode, string position, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && !string.IsNullOrEmpty(position) && !string.IsNullOrEmpty(subparameter) && list != null)
            {
                string[] auxp = null;
                int Digit = 0, CurrentNo = 0, Steps = 0;
                if (mode.ToLowerInvariant() == "digit" && subparameter.Contains(";"))
                {
                    auxp = subparameter.Split(';'); //length, start from , step
                    Digit = Convert.ToInt32(auxp[0]); //length
                    CurrentNo = Convert.ToInt32(auxp[1]); //start from
                    Steps = Convert.ToInt32(auxp[2]); //step
                }
                string filename = "", extension = "", SubLeft = "", SubRight = "";
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName);
                    switch (mode.ToLowerInvariant())
                    {
                        case "phase":
                            if (position == "0")
                            {
                                list = PrefixFileName(list, subparameter);
                                return list;
                            }
                            else if (Convert.ToInt32(position) >= filename.Length)
                            {
                                list = SuffixFileName(list, subparameter);
                                return list;
                            }
                            else
                            {
                                SubLeft = filename.Substring(0, Convert.ToInt32(position));
                                SubRight = filename.Substring(Convert.ToInt32(position));
                                list[i].FileName = SubLeft + subparameter + SubRight + extension;
                            }
                            break;
                        case "digit":
                            string strgen = CurrentNo.ToString();
                            if (CurrentNo.ToString().Length != Digit)
                            {
                                while (strgen.Length != Digit)
                                {
                                    strgen = "0" + strgen;
                                }
                            }
                            //switch (CurrentNo.ToString().Length)
                            //{
                            //    case 1:
                            //        strgen = "00" + CurrentNo.ToString();
                            //        break;
                            //    case 2:
                            //        strgen = "0" + CurrentNo.ToString();
                            //        break;
                            //    case 3:
                            //        strgen = CurrentNo.ToString();
                            //        break;
                            //}
                            if (Convert.ToInt32(position) > filename.Length)
                            {
                                position = filename.Length.ToString();
                            }
                            SubLeft = filename.Substring(0, Convert.ToInt32(position));
                            SubRight = filename.Substring(Convert.ToInt32(position));
                            list[i].FileName = SubLeft + strgen + SubRight + extension;
                            if ((CurrentNo + Steps).ToString().Length > Digit || (CurrentNo + Steps) < 0)
                            {
                                Steps = 0;
                            }
                            CurrentNo += Steps;
                            break;
                    }
                }
            }
            return list;
        }
        #endregion

        #region Extension Handling
        public static List<FileToRename> RemoveFileExtension(List<FileToRename> list, string mode, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && list != null)
            {
                string filename = "", extension = "";
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName).Substring(1);
                    switch (mode.ToLowerInvariant())
                    {
                        case "all":
                            list[i].FileName = filename;
                            break;
                        case "left":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (extension.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(extension.Length);
                                }
                                list[i].FileName = filename + "." + extension.Substring(0 + Convert.ToInt32(subparameter));
                            }
                            break;
                        case "right":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (extension.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(extension.Length);
                                }
                                list[i].FileName = filename + "." + extension.Substring(0, extension.Length - Convert.ToInt32(subparameter));
                            }
                            break;
                        case "firstletter":
                            if (extension.Length > 1)
                            {
                                list[i].FileName = filename + "." + extension.Substring(1);
                            }
                            break;
                        case "matchphase":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                list[i].FileName = filename + "." + extension.Replace(subparameter, "");
                            }
                            break;
                        case "(*)":
                            string regex = "(\\(.*\\))";
                            list[i].FileName = filename + "." + Regex.Replace(extension, regex, "");
                            break;
                        case "(*":
                            int LeftBucketIndex = extension.IndexOf('(');
                            if (LeftBucketIndex > 0)
                            {
                                list[i].FileName = filename + "." + extension.Substring(0, LeftBucketIndex);
                            }
                            break;
                        case "*)":
                            int RightBucketIndex = extension.LastIndexOf(')') + 1;
                            if (RightBucketIndex > 0)
                            {
                                list[i].FileName = filename + "." + extension.Substring(RightBucketIndex);
                            }
                            break;
                    }
                }
            }
            return list;
        }
        public static List<FileToRename> ReplaceFileExtension(List<FileToRename> list, string newExtension)
        {
            if (!string.IsNullOrEmpty(newExtension) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = list[i].FileName.Replace(Path.GetExtension(list[i].FileName).Substring(1), newExtension);
                }
            }
            return list;
        }
        public static List<FileToRename> PrefixFileExtension(List<FileToRename> list, string prefix)
        {
            if (!string.IsNullOrEmpty(prefix) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = list[i].FileName.Replace(Path.GetExtension(list[i].FileName), "." + prefix + Path.GetExtension(list[i].FileName).Substring(1));
                }
            }
            return list;
        }
        public static List<FileToRename> SuffixFileExtension(List<FileToRename> list, string suffix)
        {
            if (!string.IsNullOrEmpty(suffix) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = list[i].FileName + suffix;
                }
            }
            return list;
        }
        public static List<FileToRename> ChangeFileExtensionCase(List<FileToRename> list, string UpDown, string mode, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && list != null)
            {
                UpDown = UpDown.ToLowerInvariant();
                string filename = "", extension = "";
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName).Substring(1);
                    switch (mode.ToLowerInvariant())
                    {
                        case "all":
                            if (UpDown == "up")
                            {
                                list[i].FileName = filename + "." + extension.ToUpperInvariant();
                            }
                            else
                            {
                                list[i].FileName = filename + "." + extension.ToLowerInvariant();
                            }
                            break;
                        case "left":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (extension.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(extension.Length);
                                }
                                string ChangedLeftSlice = "";
                                if (UpDown == "up")
                                {
                                    ChangedLeftSlice = extension.Substring(0, Convert.ToInt32(subparameter)).ToUpperInvariant();
                                }
                                else
                                {
                                    ChangedLeftSlice = extension.Substring(0, Convert.ToInt32(subparameter)).ToLowerInvariant();
                                }
                                list[i].FileName = filename + "." + ChangedLeftSlice + extension.Substring(Convert.ToInt32(subparameter));
                            }
                            break;
                        case "right":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (extension.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(extension.Length);
                                }
                                string ChangedRightSlice = "";
                                if (UpDown == "up")
                                {
                                    ChangedRightSlice = extension.Substring(extension.Length - Convert.ToInt32(subparameter)).ToUpperInvariant();
                                }
                                else
                                {
                                    ChangedRightSlice = extension.Substring(extension.Length - Convert.ToInt32(subparameter)).ToLowerInvariant();
                                }
                                list[i].FileName = filename + "." + extension.Substring(0, extension.Length - Convert.ToInt32(subparameter)) + ChangedRightSlice;
                            }
                            break;
                        case "firstletter":
                            if (extension.Length > 1)
                            {
                                if (UpDown == "up")
                                {
                                    list[i].FileName = filename + "." + char.ToUpper(extension[0]) + extension.Substring(1);
                                }
                                else
                                {
                                    list[i].FileName = filename + "." + char.ToLower(extension[0]) + extension.Substring(1);
                                }
                            }
                            break;
                        case "matchphase":
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (UpDown == "up")
                                {
                                    list[i].FileName = filename + "." + extension.Replace(subparameter, subparameter.ToUpperInvariant());
                                }
                                else
                                {
                                    list[i].FileName = filename + "." + extension.Replace(subparameter, subparameter.ToLowerInvariant());
                                }
                            }
                            break;
                    }
                }
            }
            return list;
        }
        public static List<FileToRename> InsertIntoFileExtension(List<FileToRename> list, string mode, string position, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && !string.IsNullOrEmpty(position) && !string.IsNullOrEmpty(subparameter) && list != null)
            {
                string[] auxp = null;
                int Digit = 0, CurrentNo = 0, Steps = 0;
                if (mode.ToLowerInvariant() == "digit" && subparameter.Contains(";"))
                {
                    auxp = subparameter.Split(';'); //length, start from , step
                    Digit = Convert.ToInt32(auxp[0]); //length
                    CurrentNo = Convert.ToInt32(auxp[1]); //start from
                    Steps = Convert.ToInt32(auxp[2]); //step
                }
                string filename = "", extension = "", SubLeft = "", SubRight = "";
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName).Substring(1); ;
                    switch (mode.ToLowerInvariant())
                    {
                        case "phase":
                            if (position == "0")
                            {
                                list = PrefixFileExtension(list, subparameter);
                                return list;
                            }
                            else if (Convert.ToInt32(position) >= extension.Length)
                            {
                                list = SuffixFileExtension(list, subparameter);
                                return list;
                            }
                            else
                            {
                                SubLeft = extension.Substring(0, Convert.ToInt32(position));
                                SubRight = extension.Substring(Convert.ToInt32(position));
                                list[i].FileName = filename + "." + SubLeft + subparameter + SubRight;
                            }
                            break;
                        case "digit":
                            string strgen = CurrentNo.ToString(); ;
                            if (CurrentNo.ToString().Length != Digit)
                            {
                                while (strgen.Length != Digit)
                                {
                                    strgen = "0" + strgen;
                                }
                            }
                            if (Convert.ToInt32(position) > extension.Length)
                            {
                                position = extension.Length.ToString();
                            }
                            SubLeft = extension.Substring(0, Convert.ToInt32(position));
                            SubRight = extension.Substring(Convert.ToInt32(position));
                            list[i].FileName = filename + "." + SubLeft + strgen + SubRight;
                            if ((CurrentNo + Steps).ToString().Length > Digit || (CurrentNo + Steps) < 0)
                            {
                                Steps = 0;
                            }
                            CurrentNo += Steps;
                            break;
                    }
                }
            }
            return list;
        }
        #endregion
    }
}
