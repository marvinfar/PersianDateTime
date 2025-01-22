using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersianDateTime
{
    public class WFA_GetFirstAndLastDateOfMonth : CodeActivity
    {

        [Input("PersianDate yyyy/MM/dd")]
        [RequiredArgument]
        public InArgument<string> PersianDate { get; set; }

        [Input("Out Delimiter")]
        public InArgument<string> Delimiter { get; set; }

        [Output("StartDate")] 
        public OutArgument<string> StartDate { get; set; }

        [Output("EndDate")]
        public OutArgument<string> EndDate { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IExecutionContext extension = executionContext.GetExtension<IExecutionContext>();
            IOrganizationServiceFactory extension2 = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService organizationService = extension2.CreateOrganizationService(extension.UserId);
            ITracingService extension3 = executionContext.GetExtension<ITracingService>();


            string inputDate = PersianDate.Get<string>(executionContext);
            string delimiter = Delimiter.Get<string>(executionContext);
            var dateFunc = new Helper();

            if (string.IsNullOrEmpty(delimiter))
                delimiter = "/";
            else
                delimiter = delimiter.Trim().Substring(0, 1);

            try
            {
                // تبدیل تاریخ ورودی به تاریخ میلادی
                PersianCalendar persianCalendar = new PersianCalendar();
                DateTime persianDate = dateFunc.ParsePersianDate(inputDate, persianCalendar);

                // محاسبه اولین و آخرین روز ماه
                string firstDayOfMonth = dateFunc.GetFirstDayOfMonth(persianDate, persianCalendar);
                string lastDayOfMonth = dateFunc.GetLastDayOfMonth(persianDate, persianCalendar);

                // نمایش نتایج
                executionContext.SetValue(StartDate, firstDayOfMonth.Replace("/",delimiter));
                executionContext.SetValue(EndDate, lastDayOfMonth.Replace("/",delimiter));
            }
            catch 
            {
                executionContext.SetValue(StartDate, null);
                executionContext.SetValue(EndDate, null);
            }
        }

    }

    public class WFA_AddMonth : CodeActivity
    {

        [Input("PersianDate yyyy/MM/dd")]
        [RequiredArgument]
        public InArgument<string> PersianDate { get; set; }

        [Input("Out Delimiter")]
        public InArgument<string> Delimiter { get; set; }

        [Input("Number")]
        [RequiredArgument]
        public InArgument<int> Number { get; set; }

        [Output("NewDate")]
        public OutArgument<string> NewDate { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IExecutionContext extension = executionContext.GetExtension<IExecutionContext>();
            IOrganizationServiceFactory extension2 = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService organizationService = extension2.CreateOrganizationService(extension.UserId);
            ITracingService extension3 = executionContext.GetExtension<ITracingService>();


            string inputDate = PersianDate.Get<string>(executionContext);
            string delimiter = Delimiter.Get<string>(executionContext);
            int number = Number.Get(executionContext);

            if (string.IsNullOrEmpty(delimiter))
                delimiter = "/";
            else
                delimiter = delimiter.Trim().Substring(0, 1);

            var dateFunc = new Helper();
            try
            {
                // تبدیل تاریخ ورودی به تاریخ میلادی
                PersianCalendar persianCalendar = new PersianCalendar();
                DateTime persianDate = dateFunc.ParsePersianDate(inputDate, persianCalendar);

                // کاهش یک ماه از تاریخ
                DateTime newDate = dateFunc.AddMonth(persianDate, persianCalendar,number);

                // نمایش نتایج
                executionContext.SetValue(NewDate, dateFunc.ConvertToPersianDate(newDate, persianCalendar).Replace("/",delimiter));
            }
            catch 
            {
                executionContext.SetValue(NewDate, null);
            }
        }

    }

    public class WFA_AddYears : CodeActivity
    {

        [Input("PersianDate yyyy/MM/dd")]
        [RequiredArgument]
        public InArgument<string> PersianDate { get; set; }

        [Input("Out Delimiter")]
        public InArgument<string> Delimiter { get; set; }

        [Input("Number")]
        [RequiredArgument]
        public InArgument<int> Number { get; set; }

        [Output("NewDate")]
        public OutArgument<string> NewDate { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IExecutionContext extension = executionContext.GetExtension<IExecutionContext>();
            IOrganizationServiceFactory extension2 = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService organizationService = extension2.CreateOrganizationService(extension.UserId);
            ITracingService extension3 = executionContext.GetExtension<ITracingService>();


            string inputDate = PersianDate.Get<string>(executionContext);
            string delimiter = Delimiter.Get<string>(executionContext);
            int number = Number.Get(executionContext);

            if (string.IsNullOrEmpty(delimiter))
                delimiter = "/";
            else
                delimiter = delimiter.Trim().Substring(0, 1);

            var dateFunc = new Helper();
            try
            {
                // تبدیل تاریخ ورودی به تاریخ میلادی
                PersianCalendar persianCalendar = new PersianCalendar();
                // کاهش یا افزایش سال از تاریخ
                string newDate = dateFunc.AddYears(inputDate, persianCalendar, number);

                // نمایش نتایج
                executionContext.SetValue(NewDate, newDate.Replace("/", delimiter));
            }
            catch 
            {
                executionContext.SetValue(NewDate, null);
            }
        }

    }

    public class WFA_AddDays : CodeActivity
    {

        [Input("PersianDate yyyy/MM/dd")]
        [RequiredArgument]
        public InArgument<string> PersianDate { get; set; }

        [Input("Out Delimiter")]
        public InArgument<string> Delimiter { get; set; }

        [Input("Number")]
        [RequiredArgument]
        public InArgument<int> Number { get; set; }

        [Output("NewDate")]
        public OutArgument<string> NewDate { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IExecutionContext extension = executionContext.GetExtension<IExecutionContext>();
            IOrganizationServiceFactory extension2 = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService organizationService = extension2.CreateOrganizationService(extension.UserId);
            ITracingService extension3 = executionContext.GetExtension<ITracingService>();


            string inputDate = PersianDate.Get<string>(executionContext);
            string delimiter = Delimiter.Get<string>(executionContext);
            int number = Number.Get(executionContext);

            if (string.IsNullOrEmpty(delimiter))
                delimiter = "/";
            else
                delimiter = delimiter.Trim().Substring(0, 1);

            var dateFunc = new Helper();
            try
            {
                // تبدیل تاریخ ورودی به تاریخ میلادی
                PersianCalendar persianCalendar = new PersianCalendar();
                // کاهش یا افزایش سال از تاریخ
                string newDate = dateFunc.AddDays(inputDate, persianCalendar, number);

                // نمایش نتایج
                executionContext.SetValue(NewDate, newDate.Replace("/", delimiter));
            }
            catch 
            {
                executionContext.SetValue(NewDate, null);
            }
        }

    }

    public class WFA_PersianToMiladi : CodeActivity
    {

        [Input("PersianDate")]
        [RequiredArgument]
        public InArgument<string> PersianDate { get; set; }

        [Input("Delimiter")]
        public InArgument<string> Delimiter { get; set; }

        [Output("MiladiDate")] public OutArgument<string> MiladiDate { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IExecutionContext extension = executionContext.GetExtension<IExecutionContext>();
            IOrganizationServiceFactory extension2 = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService organizationService = extension2.CreateOrganizationService(extension.UserId);
            ITracingService extension3 = executionContext.GetExtension<ITracingService>();

            string inputDate = PersianDate.Get<string>(executionContext);
            string delimiter = Delimiter.Get<string>(executionContext);

            if (string.IsNullOrEmpty(delimiter))
                delimiter = "/";
            else
                delimiter=delimiter.Trim().Substring(0, 1);
            try
            {
                var dateFunc = new Helper();
                // تبدیل تاریخ ورودی به تاریخ میلادی
                PersianCalendar persianCalendar = new PersianCalendar();
                string gregorianDate = dateFunc.ConvertToGregorianDate(inputDate, persianCalendar).Replace("/",delimiter);
                // نمایش تاریخ میلادی
                executionContext.SetValue(MiladiDate, gregorianDate);
            }
            catch 
            {
                executionContext.SetValue(MiladiDate, null);
            }
            
        }


    }

    public class WFA_MiladiToPersian: CodeActivity
    {

        [Input("Miladi yyyy/MM/dd")]
        [RequiredArgument]
        public InArgument<string> MiladiDate { get; set; }

        [Input("Delimiter")]
        public InArgument<string> Delimiter { get; set; }

        [Output("PersianDate")] 
        public OutArgument<string> PersianDate { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IExecutionContext extension = executionContext.GetExtension<IExecutionContext>();
            IOrganizationServiceFactory extension2 = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService organizationService = extension2.CreateOrganizationService(extension.UserId);
            ITracingService extension3 = executionContext.GetExtension<ITracingService>();

            string inputDate= MiladiDate.Get<string>(executionContext);
            string delimiter = Delimiter.Get<string>(executionContext);

            
            if (string.IsNullOrEmpty(delimiter))
                delimiter = "/";
            else
                delimiter = delimiter.Trim().Substring(0, 1);
            try
            {
                // تبدیل تاریخ ورودی به تاریخ میلادی
                DateTime gregorianDate = DateTime.ParseExact(inputDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                var dateFunc = new Helper();
                // تبدیل تاریخ میلادی به شمسی
                PersianCalendar persianCalendar = new PersianCalendar();
                string persianDate = dateFunc.ConvertToPersianDate(gregorianDate, persianCalendar);

                // نمایش تاریخ شمسی
                executionContext.SetValue(PersianDate, persianDate.Replace("/",delimiter));
            }
            catch 
            {
                executionContext.SetValue(PersianDate, null);
            }
        }

    }

    public class WFA_ToDate : CodeActivity
    {

        [Input("MiladiDate yyyy/MM/dd")]
        [RequiredArgument]
        public InArgument<string> MiladiDate { get; set; }

        [Output("Date")]
        public OutArgument<DateTime> Date { get; set; }


        protected override void Execute(CodeActivityContext executionContext)
        {
            IExecutionContext extension = executionContext.GetExtension<IExecutionContext>();
            IOrganizationServiceFactory extension2 = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService organizationService = extension2.CreateOrganizationService(extension.UserId);
            ITracingService extension3 = executionContext.GetExtension<ITracingService>();

            string inputDate = MiladiDate.Get<string>(executionContext);

            try
            {
                // نمایش تاریخ میلادی
                executionContext.SetValue(Date, Convert.ToDateTime(inputDate));
            }
            catch 
            {
                executionContext.SetValue(Date, null);
            }

        }


    }

    public class WFA_PersianDateDetail : CodeActivity
    {

        [Input("PersianDate yyyy/MM/dd")]
        [RequiredArgument]
        public InArgument<string> PersianDate { get; set; }

        [Output("DetailDate")] 
        public OutArgument<string> DetailDate { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IExecutionContext extension = executionContext.GetExtension<IExecutionContext>();
            IOrganizationServiceFactory extension2 = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService organizationService = extension2.CreateOrganizationService(extension.UserId);
            ITracingService extension3 = executionContext.GetExtension<ITracingService>();

            string inputDate = PersianDate.Get<string>(executionContext);
            
            try
            {
                var dateFunc = new Helper();
                // تبدیل تاریخ ورودی به تاریخ میلادی
                PersianCalendar persianCalendar = new PersianCalendar();
                string detailedPersianDate = dateFunc.GetDetailedPersianDate(inputDate, persianCalendar);
                // نمایش تاریخ میلادی
                executionContext.SetValue(DetailDate, detailedPersianDate);
            }
            catch 
            {
                executionContext.SetValue(DetailDate, null);
            }

        }


    }

    public class WFA_DateDifference : CodeActivity
    {

        [Input("PersianDate1")]
        [RequiredArgument]
        public InArgument<string> PersianDate1 { get; set; }

        [Input("PersianDate2")]
        [RequiredArgument]
        public InArgument<string> PersianDate2 { get; set; }

        [Output("TotalDays")] public OutArgument<int> TotalDays { get; set; }
        [Output("RemainDays")] public OutArgument<int> RemainDays { get; set; }
        [Output("Years")] public OutArgument<int> Years { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IExecutionContext extension = executionContext.GetExtension<IExecutionContext>();
            IOrganizationServiceFactory extension2 = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService organizationService = extension2.CreateOrganizationService(extension.UserId);
            ITracingService extension3 = executionContext.GetExtension<ITracingService>();

            string firstDate = PersianDate1.Get<string>(executionContext);
            string secondDate = PersianDate2.Get<string>(executionContext);

            try
            {
                var dateFunc = new Helper();
                // تبدیل تاریخ ورودی به تاریخ میلادی
                PersianCalendar persianCalendar = new PersianCalendar();
                Tuple<int,int,int> tuple = new Tuple<int,int, int >(0,0,0);
                
                tuple = dateFunc.GetPersianDateDifference(firstDate, secondDate,persianCalendar);
                // نمایش تاریخ میلادی
                executionContext.SetValue(TotalDays, tuple.Item1);
                executionContext.SetValue(Years, tuple.Item2);
                executionContext.SetValue(RemainDays, tuple.Item3);
            }
            catch
            {
                executionContext.SetValue(RemainDays, 0);
                executionContext.SetValue(RemainDays, 0);
                executionContext.SetValue(RemainDays, 0);
            }

        }


    }

    public class WFA_SplitPersianDate : CodeActivity
    {

        [Input("PersianDate")]
        [RequiredArgument]
        public InArgument<string> PersianDate {get; set;}

        [Input("Delimiter")]
        public InArgument<string> Delimiter { get; set; }

        [Output("Year")] public OutArgument<int> Year { get; set; }
        [Output("Month")] public OutArgument<int> Month { get; set; }
        [Output("Day")] public OutArgument<int> Day { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IExecutionContext extension = executionContext.GetExtension<IExecutionContext>();
            IOrganizationServiceFactory extension2 = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService organizationService = extension2.CreateOrganizationService(extension.UserId);
            ITracingService extension3 = executionContext.GetExtension<ITracingService>();

            string inputDate = PersianDate.Get<string>(executionContext);
            string delimiter = Delimiter.Get<string>(executionContext);

            if (string.IsNullOrEmpty(delimiter))
                delimiter = "/";
            else
                delimiter = delimiter.Trim().Substring(0, 1);
            try
            {
                PersianCalendar p = new PersianCalendar();
                var parts = inputDate.Split(Char.Parse(delimiter));
                int year = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int day = int.Parse(parts[2]);
                
                executionContext.SetValue(Year, year);
                executionContext.SetValue(Month, month);
                executionContext.SetValue(Day, day);
            }
            catch
            {
                executionContext.SetValue(Year, 0);
                executionContext.SetValue(Month, 0);
                executionContext.SetValue(Day, 0);
            }

        }


    }
}
