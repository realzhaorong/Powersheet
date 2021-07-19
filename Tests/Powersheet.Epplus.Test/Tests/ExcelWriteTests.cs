using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Nerosoft.Powersheet.Epplus.Test
{
    public class ExcelWriteTests
    {
        private readonly ISheetWrapper _wrapper;
        private static readonly string TempFileDirectory = Path.Combine(AppContext.BaseDirectory, "Temps");

        public ExcelWriteTests(ISheetWrapper wrapper)
        {
            _wrapper = wrapper;
            if (!Directory.Exists(TempFileDirectory))
            {
                Directory.CreateDirectory(TempFileDirectory);
            }
        }

        [Fact]
        public async Task TestWriteFromDataTableAsync_CN()
        {
            #region 构建数据

            var dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Gender");
            dataTable.Columns.Add("Age");
            dataTable.Columns.Add("Birthdate");
            dataTable.Columns.Add("Department");
            dataTable.Columns.Add("IsActive");
            {
                var row = dataTable.NewRow();
                row["Name"] = "张三";
                row["Gender"] = 1;
                row["Age"] = 32;
                row["Birthdate"] = DateTime.Parse("1989/6/9");
                row["Department"] = "研发部";
                row["IsActive"] = true;
                dataTable.Rows.Add(row);
            }
            {
                var row = dataTable.NewRow();
                row["Name"] = "李四";
                row["Gender"] = 1;
                row["Age"] = 26;
                row["Birthdate"] = DateTime.Parse("1996/12/2");
                row["Department"] = "销售部";
                row["IsActive"] = false;
                dataTable.Rows.Add(row);
            }
            {
                var row = dataTable.NewRow();
                row["Name"] = "王小丫";
                row["Gender"] = 2;
                row["Age"] = 28;
                row["Birthdate"] = DateTime.Parse("1993/4/22");
                row["Department"] = "法务部";
                row["IsActive"] = true;
                dataTable.Rows.Add(row);
            }

            #endregion

            var options = new SheetWriteOptions();

            options.AddMapProfile("Id", "编号");
            options.AddMapProfile("Name", "姓名");
            options.AddMapProfile("Gender", "性别", (value, _) =>
            {
                return value switch
                {
                    1 => "男",
                    "1" => "男",
                    2 => "女",
                    "2" => "女",
                    _ => ""
                };
            });
            options.AddMapProfile("Age", "年龄");
            options.AddMapProfile("Birthdate", "出生日期");
            options.AddMapProfile("Department", "部门");
            options.AddMapProfile("IsActive", "是否在职", (value, _) => IsActiveValueConvert(value));

            var stream = await _wrapper.WriteAsync(dataTable, options, "职员表");
            Assert.NotEqual(0, stream.Length);
            using (stream)
            {
                var buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer.AsMemory(0, buffer.Length));
                await File.WriteAllBytesAsync(Path.Combine(TempFileDirectory, "TestWriteFromDataTableAsync_CN.xlsx"), buffer);
            }
        }

        [Fact]
        public async Task TestWriteFromObjectListAsync_CN()
        {
            var employees = new List<Employee>
            {
                new() {Name = "张三", Gender = 1, Age = 32, Birthdate = DateTime.Parse("1989/6/9"), Department = "研发部", IsActive = true},
                new() {Name = "李四", Gender = 1, Age = 26, Birthdate = DateTime.Parse("1996/12/2"), Department = "销售部", IsActive = false},
                new() {Name = "王小丫", Gender = 1, Age = 28, Birthdate = DateTime.Parse("1993/4/22"), Department = "法务部", IsActive = true}
            };

            var options = new SheetWriteOptions();

            options.UseMapProfile<Employee>(t => t.Id, "编号")
                   .UseMapProfile<Employee>(t => t.Name, "姓名")
                   .UseMapProfile<Employee>(t => t.Gender, "性别", (value, _) =>
                   {
                       return value switch
                       {
                           1 => "男",
                           "1" => "男",
                           2 => "女",
                           "2" => "女",
                           _ => ""
                       };
                   })
                   .UseMapProfile<Employee>(t => t.Age, "年龄")
                   .UseMapProfile<Employee>(t => t.Birthdate, "出生日期")
                   .UseMapProfile<Employee>(t => t.Department, "部门")
                   .UseMapProfile<Employee>(t => t.IsActive, "是否在职", (value, _) => IsActiveValueConvert(value));

            var stream = await _wrapper.WriteAsync(employees, options, "职员表");
            Assert.NotEqual(0, stream.Length);
            using (stream)
            {
                var buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer.AsMemory(0, buffer.Length));
                await File.WriteAllBytesAsync(Path.Combine(TempFileDirectory, "TestWriteFromObjectListAsync_CN.xlsx"), buffer);
            }
        }

        private static string IsActiveValueConvert(object value)
        {
            return value switch
            {
                true => "是",
                "true" => "是",
                "True" => "是",
                false => "否",
                "false" => "否",
                "False" => "否",
                _ => ""
            };
        }
    }
}