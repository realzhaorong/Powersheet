using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Nerosoft.Powersheet
{
    /// <summary>
    /// 表单内容解析契约接口
    /// </summary>
    public interface ISheetWrapper
    {
        /// <summary>
        /// 读取表格内容到<see cref="DataTable"/>。
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="options">配置选项</param>
        /// <param name="sheetIndex">表格位置索引，起始值为0</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DataTable> ReadToDataTableAsync(string file, SheetReadOptions options, int sheetIndex, CancellationToken cancellationToken = default);

        /// <summary>
        /// 读取表格内容到<see cref="DataTable"/>。
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="options">配置选项</param>
        /// <param name="sheetName">表格名称，不指定取第一个</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DataTable> ReadToDataTableAsync(string file, SheetReadOptions options, string sheetName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 读取表格内容到<see cref="DataTable"/>。
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">配置选项</param>
        /// <param name="sheetIndex">表格位置索引，起始值为0</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DataTable> ReadToDataTableAsync(Stream stream, SheetReadOptions options, int sheetIndex, CancellationToken cancellationToken = default);

        /// <summary>
        /// 读取表格内容到<see cref="DataTable"/>。
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">配置选项</param>
        /// <param name="sheetName">表格名称，不指定取第一个</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DataTable> ReadToDataTableAsync(Stream stream, SheetReadOptions options, string sheetName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 读取表格内容到对象集合
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="options">配置选项</param>
        /// <param name="sheetIndex">表格位置索引，起始值为0</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">结果对象类型，必须是类且包含公开的无参构造器</typeparam>
        /// <returns></returns>
        Task<List<T>> ReadToListAsync<T>(string file, SheetReadOptions options, int sheetIndex, CancellationToken cancellationToken = default)
            where T : class, new();

        /// <summary>
        /// 读取表格内容到对象集合
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="options"></param>
        /// <param name="sheetName">表格名称，不指定取第一个</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">结果对象类型，必须是类且包含公开的无参构造器</typeparam>
        /// <returns></returns>
        Task<List<T>> ReadToListAsync<T>(string file, SheetReadOptions options, string sheetName, CancellationToken cancellationToken = default)
            where T : class, new();

        /// <summary>
        /// 读取表格内容到对象集合
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">配置选项</param>
        /// <param name="sheetIndex">表格位置索引，起始值为0</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">结果对象类型，必须是类且包含公开的无参构造器</typeparam>
        /// <returns></returns>
        Task<List<T>> ReadToListAsync<T>(Stream stream, SheetReadOptions options, int sheetIndex, CancellationToken cancellationToken = default)
            where T : class, new();

        /// <summary>
        /// 读取表格内容到对象集合
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">配置选项</param>
        /// <param name="sheetName">表格名称，不指定取第一个</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">结果对象类型，必须是类且包含公开的无参构造器</typeparam>
        /// <returns></returns>
        Task<List<T>> ReadToListAsync<T>(Stream stream, SheetReadOptions options, string sheetName, CancellationToken cancellationToken = default)
            where T : class, new();

        /// <summary>
        /// 读取指定单列数据到集合
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="firstRowNumber">起始行号，从1开始</param>
        /// <param name="columnNumber">列号，起始值为1</param>
        /// <param name="sheetIndex">表格位置索引，起始值为0</param>
        /// <param name="valueConvert">值转换方法</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <returns></returns>
        Task<List<T>> ReadToListAsync<T>(string file, int firstRowNumber, int columnNumber, int sheetIndex, Func<object, CultureInfo, T> valueConvert = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 读取指定单列数据到集合
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="firstRowNumber">起始行号，从1开始</param>
        /// <param name="columnNumber">列号，起始值为1</param>
        /// <param name="sheetIndex">表格位置索引，起始值为0</param>
        /// <param name="valueConvert">值转换方法</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <returns></returns>
        Task<List<T>> ReadToListAsync<T>(Stream stream, int firstRowNumber, int columnNumber, int sheetIndex, Func<object,  CultureInfo, T> valueConvert = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 读取指定单列数据到集合
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="firstRowNumber">起始行号，从1开始</param>
        /// <param name="columnNumber">列号，起始值为1</param>
        /// <param name="sheetName">表格名称，不指定取第一个</param>
        /// <param name="valueConvert">值转换方法</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <returns></returns>
        Task<List<T>> ReadToListAsync<T>(string file, int firstRowNumber, int columnNumber, string sheetName, Func<object, CultureInfo, T> valueConvert = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 读取指定单列数据到集合
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="firstRowNumber">起始行号，从1开始</param>
        /// <param name="columnNumber">列号，起始值为1</param>
        /// <param name="sheetName">表格名称，不指定取第一个</param>
        /// <param name="valueConvert">值转换方法</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <returns></returns>
        Task<List<T>> ReadToListAsync<T>(Stream stream, int firstRowNumber, int columnNumber, string sheetName, Func<object, CultureInfo, T> valueConvert = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 将DataTable数据写入到表格并返回流。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="options">配置选项</param>
        /// <param name="sheetName">表格名称，不指定取第一个</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Stream> WriteAsync(DataTable data, SheetWriteOptions options, string sheetName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 将对象写入到表格并返回流。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="options">配置选项</param>
        /// <param name="sheetName">表格名称，不指定取第一个</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <returns></returns>
        Task<Stream> WriteAsync<T>(IEnumerable<T> data, SheetWriteOptions options, string sheetName, CancellationToken cancellationToken = default)
            where T : class, new();

        /// <summary>
        /// 将对象写入到表格并返回流
        /// </summary>
        /// <param name="dataFactory"></param>
        /// <param name="options">配置选项</param>
        /// <param name="sheetName">表格名称，不指定取第一个</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <returns></returns>
        Task<Stream> WriteAsync<T>(Func<Task<IEnumerable<T>>> dataFactory, SheetWriteOptions options, string sheetName, CancellationToken cancellationToken = default)
            where T : class, new();

        /// <summary>
        /// 将数据集合写入到表格的指定列并返回流。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="firstRowNumber">起始行号，从1开始</param>
        /// <param name="columnNumber">列号，起始值为1</param>
        /// <param name="valueConvert">值转换方法</param>
        /// <param name="sheetName">表格名称，不指定取第一个</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <returns></returns>
        Task<Stream> WriteAsync<T>(IEnumerable<T> data, int firstRowNumber, int columnNumber, string sheetName, Func<T, CultureInfo, object> valueConvert = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 将数据集合写入到表格的指定列并返回流
        /// </summary>
        /// <param name="dataFactory"></param>
        /// <param name="firstRowNumber">起始行号，从1开始</param>
        /// <param name="columnNumber">列号，起始值为1</param>
        /// <param name="sheetName">表格名称，不指定取第一个</param>
        /// <param name="valueConvert">值转换方法</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <returns></returns>
        Task<Stream> WriteAsync<T>(Func<Task<IEnumerable<T>>> dataFactory, int firstRowNumber, int columnNumber, string sheetName, Func<T, CultureInfo, object> valueConvert = null, CancellationToken cancellationToken = default);
    }
}