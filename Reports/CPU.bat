wmic /output:%1.txt path win32_perfformatteddata_perfproc_process where (PercentProcessorTime ^> 0) get Name, Caption, PercentProcessorTime, IDProcess /format:list
