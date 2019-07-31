using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

//using PortableDeviceApiLib;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace WinAppDemo.Controls
{
    public partial class UcZjtq_SJ_Ljcg : UserControl
    {

        //PortableDeviceManagerClass devMgr = new PortableDeviceApiLib.PortableDeviceManagerClass();
        //IPortableDeviceValues pValues = (IPortableDeviceValues)new PortableDeviceTypesLib.PortableDeviceValuesClass();
        //PortableDeviceClass pPortableDevice = new PortableDeviceClass();
        //PortableDeviceClass ppDevice = new PortableDeviceClass();
        string deviceID = string.Empty;


        public UcZjtq_SJ_Ljcg()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();

            AppContext.GetInstance().m_ucZjtq_sj_qz1.Dock = DockStyle.Fill;
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(AppContext.GetInstance().m_ucZjtq_sj_qz1);
        }

        /// <summary>
        /// 检测PC连接设备信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void UcZjtq_SJ_Ljcg_Load(object sender, EventArgs e)
        {
           
             label2.Text = Program.m_mainform.DeviceBrand + "--" + Program.m_mainform.DeviceModel;
            label3.Text = "Android" + Program.m_mainform.Devicesystem;
            label4 .Text= Program.m_mainform.DeviceState;


        }



        //#region
        ///// <summary>
        ///// 枚举所有便携式设备（MTP模式）
        ///// </summary>
        ///// <returns>返回设备id数组</returns>
        //public string[] EnumerateDevices()
        //{
        //    string[] devicesIds = null;
        //    PortableDeviceManagerClass deviceManager = new PortableDeviceManagerClass();
        //    uint deviceCount = 1;//设备数目初始值必须大于0
        //    deviceManager.GetDevices(null, ref deviceCount);//获取设备数目必须置第一个参数为null
        //    if (deviceCount > 0)
        //    {
        //        devicesIds = new string[deviceCount];
        //        Console.WriteLine(devicesIds);
        //        deviceManager.GetDevices(devicesIds, ref deviceCount);
        //    }
        //    return devicesIds;
        //}

        ///// <summary>
        ///// 连接设备
        ///// </summary>
        ///// <param name="DeviceId"></param>
        ///// <param name="portableDevice"></param>
        ///// <param name="deviceContent"></param>
        ///// <returns></returns>
        //public IPortableDeviceValues Connect(string DeviceId, out PortableDevice portableDevice, out IPortableDeviceContent deviceContent)
        //{
        //    IPortableDeviceValues clientInfo = (IPortableDeviceValues)new PortableDeviceTypesLib.PortableDeviceValuesClass();
        //    portableDevice = new PortableDeviceClass();
        //    portableDevice.Open(DeviceId, clientInfo);
        //    portableDevice.Content(out deviceContent);

        //    IPortableDeviceProperties deviceProperties;
        //    deviceContent.Properties(out deviceProperties);

        //    IPortableDeviceValues deviceValues;
        //    deviceProperties.GetValues("DEVICE", null, out deviceValues);
        //    return deviceValues;
        //}
        ///// <summary>
        ///// 断开设备
        ///// </summary>
        ///// <param name="portableDevice"></param>
        //public void Disconnect(PortableDevice portableDevice)
        //{
        //    portableDevice.Close();
        //}

        ///// <summary>
        ///// 设备类型
        ///// </summary>
        //public enum DeviceType
        //{
        //    Generic = 0,
        //    Camera = 1,
        //    MediaPlayer = 2,
        //    Phone = 3,
        //    Video = 4,
        //    PersonalInformationManager = 5,
        //    AudioRecorder = 6
        //};
        ///// <summary>
        ///// 获取设备类型
        ///// </summary>
        ///// <param name="DeviceValues"></param>
        ///// <returns></returns>
        //public DeviceType GetDeviceType(IPortableDeviceValues DeviceValues)
        //{
        //    _tagpropertykey deviceTypeKey = new _tagpropertykey() { fmtid = new Guid("26d4979a-e643-4626-9e2b-736dc0c92fdc"), pid = 15 };
        //    uint propertyValue;
        //    DeviceValues.GetUnsignedIntegerValue(ref deviceTypeKey, out propertyValue);
        //    DeviceType deviceType = (DeviceType)propertyValue;
        //    return deviceType;
        //}
        ///// <summary>
        ///// 获取设备名
        ///// </summary>
        ///// <param name="DeviceValues"></param>
        ///// <returns></returns>
        //public string GetDeviceName(IPortableDeviceValues DeviceValues)
        //{
        //    _tagpropertykey property = new _tagpropertykey() { fmtid = new Guid("ef6b490d-5cd8-437a-affc-da8b60ee4a3c"), pid = 4 };
        //    string name;
        //    DeviceValues.GetStringValue(ref property, out name);
        //    return name;
        //}
        ///// <summary>
        ///// 获取固件版本
        ///// </summary>
        ///// <param name="DeviceValues"></param>
        ///// <returns></returns>
        //public string GetFirmwareVersion(IPortableDeviceValues DeviceValues)
        //{
        //    _tagpropertykey deviceTypeKey = new _tagpropertykey() { fmtid = new Guid("26d4979a-e643-4626-9e2b-736dc0c92fdc"), pid = 3 };
        //    string firmwareVersion;
        //    DeviceValues.GetStringValue(ref deviceTypeKey, out firmwareVersion);
        //    return firmwareVersion;
        //}
        ///// <summary>
        ///// 获取制造商
        ///// </summary>
        ///// <param name="DeviceValues"></param>
        ///// <returns></returns>
        //public string GetManufacturer(IPortableDeviceValues DeviceValues)
        //{
        //    _tagpropertykey property = new _tagpropertykey() { fmtid = new Guid("26d4979a-e643-4626-9e2b-736dc0c92fdc"), pid = 7 };
        //    string manufacturer;
        //    DeviceValues.GetStringValue(ref property, out manufacturer);
        //    return manufacturer;
        //}
        ///// <summary>
        ///// 获取型号
        ///// </summary>
        ///// <param name="DeviceValues"></param>
        ///// <returns></returns>
        //public string GetModel(IPortableDeviceValues DeviceValues)
        //{
        //    _tagpropertykey property = new _tagpropertykey()
        //    {
        //        fmtid = new Guid("26d4979a-e643-4626-9e2b-736dc0c92fdc"),
        //        pid = 8
        //    };
        //    string model;
        //    DeviceValues.GetStringValue(ref property, out model);
        //    return model;
        //}
        ///// <summary>
        ///// 获取设备或设备下文件夹的所有对象（文件、文件夹）的ObjectId
        ///// </summary>
        ///// <param name="deviceId"></param>
        ///// <param name="parentId"></param>
        ///// <returns></returns>
        //public List<string> GetChildrenObjectIds(IPortableDeviceContent content, string parentId)
        //{
        //    IEnumPortableDeviceObjectIDs objectIds;
        //    content.EnumObjects(0, parentId, null, out objectIds);
        //    List<string> childItems = new List<string>();
        //    uint fetched = 0;
        //    do
        //    {
        //        string objectId;
        //        objectIds.Next(1, out objectId, ref fetched);
        //        if (fetched > 0)
        //        {
        //            childItems.Add(objectId);
        //        }
        //    }
        //    while (fetched > 0);
        //    return childItems;
        //}
        ///// <summary>
        ///// 获取总容量和可用容量
        ///// </summary>
        ///// <param name="deviceContent"></param>
        ///// <param name="storageId"></param>
        ///// <param name="freeSpace"></param>
        ///// <param name="storageCapacity"></param>
        //public void GetStorageCapacityAnFreeSpace(IPortableDeviceContent deviceContent, string storageId, out ulong freeSpace, out ulong storageCapacity)
        //{
        //    IPortableDeviceProperties deviceProperties;
        //    deviceContent.Properties(out deviceProperties);

        //    IPortableDeviceKeyCollection keyCollection = (IPortableDeviceKeyCollection)new PortableDeviceTypesLib.PortableDeviceKeyCollectionClass();
        //    _tagpropertykey freeSpaceKey = new _tagpropertykey();
        //    freeSpaceKey.fmtid = new Guid("01a3057a-74d6-4e80-bea7-dc4c212ce50a");
        //    freeSpaceKey.pid = 5;

        //    _tagpropertykey storageCapacityKey = new _tagpropertykey();
        //    storageCapacityKey.fmtid = new Guid("01a3057a-74d6-4e80-bea7-dc4c212ce50a");
        //    storageCapacityKey.pid = 4;

        //    keyCollection.Add(ref freeSpaceKey);
        //    keyCollection.Add(ref storageCapacityKey);

        //    IPortableDeviceValues deviceValues;
        //    deviceProperties.GetValues(storageId, keyCollection, out deviceValues);

        //    deviceValues.GetUnsignedLargeIntegerValue(ref freeSpaceKey, out freeSpace);
        //    deviceValues.GetUnsignedLargeIntegerValue(ref storageCapacityKey, out storageCapacity);
        //}

        //#endregion

    }
}
