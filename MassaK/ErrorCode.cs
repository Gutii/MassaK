using System;
using System.Collections.Generic;
using System.Text;

namespace ASHK.MassaK
{
    enum ErrorCode
    {
        /// <summary>
        /// Операция выполнена успешно (ошибок нет)
        /// </summary>
        Success = 0,                        
        /// <summary>
        /// Драйвер занят
        /// </summary>
        Byse = -1,                          
        /// <summary>
        /// Файл не найден
        /// </summary>
        FileNotFound = -2,                  
        /// <summary>
        /// Терминал не найден
        /// </summary>
        TerminalNotFound = -3,              
        /// <summary>
        /// Ошибка подключения к терминалу
        /// </summary>
        TerminalErrorConnect = -4,          
        /// <summary>
        /// Ошибка передачи данных в терминал
        /// </summary>
        TerminalSendingErrorData = -5,      
        /// <summary>
        /// Ошибка приема данных в терминал
        /// </summary>
        TerminalReceivingErrorData = -6,    
        /// <summary>
        /// Ошибка обработки данных регистрации
        /// </summary>
        RegistrationErrorData = -7,         
        /// <summary>
        /// Ошибка установки режима работы драйвера
        /// </summary>
        DriverErrorMode = -8,               
        /// <summary>
        /// Данный код ошибки не используется в текущей версии драйвера
        /// </summary>
        NoError = -9,                       
        /// <summary>
        /// Нет места для записи данных в буфер драйвера
        /// </summary>
        NoSpaceBufferDriver = -10,          
        /// <summary>
        /// Не хватает памяти в терминале для загрузки такого объема данных
        /// </summary>
        NoMemoryTerminal = -11,             
        /// <summary>
        /// В справочнике больше нет записей
        /// </summary>
        NoRecords = -12,                    
        /// <summary>
        /// На USB-flash больше нет файлов регистраций
        /// </summary>
        NoFilesUSB = -13,                   
        /// <summary>
        /// Идет сжатие данных, пожалуйста, подождите
        /// </summary>
        WaitCompress = -14,
        /// <summary>
        /// Сжатие данных завершено
        /// </summary>
        CompressCompleted = -15,
        /// <summary>
        /// Неверное значение свойства
        /// </summary>
        InvalidPropertyValue = -1000,
        /// <summary>
        /// Данный код ошибки не используется в текущей версии драйвера
        /// </summary>
        NoError1 = -1001
    }
}
