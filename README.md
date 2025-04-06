## 平行處理資料載入/寫入速度

## 測量設備
### CPU i7-118000H 
### Threads:16
### P-Core:8 E-Core:0
### RAM 16GB

順便看一下最終消耗的記憶體用量
### 實驗0
#### 實驗0-1(單純讀取)：

| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|------|------------|
| 10,000 | 0.031 | 13 |
| 50,000 | 0.212 | 26 |
| 100,000 | 0.365 | 38 |
| 200,000 | 0.741 | 60 |
| 500,000 | 1.707 | 131 |
| 1,000,000 | 3.216 | 248 |
| 2,000,000 | 6.064 | 486 |
| 3,000,000 | 9.185 | 722 |
| 5,000,000 | 15.714 | 1200 |
| 10,000,000 | 42.569 | 2300 |

#### 實驗0-2(讀取+寫入)：
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 10,000 | 0.1 | 20 |
| 50,000 | 0.323 | 29 |
| 100,000 | 0.546 | 40 |
| 200,000 | 1.043 | 64 |
| 500,000 | 2.418 | 133 |
| 1,000,000 | 4.581 | 250 |
| 2,000,000 | 9.297 | 490 |
| 3,000,000 | 13.934 | 725 |
| 5,000,000 | 23.853| 1200 |
| 10,000,000 | 220 | 2300 |

### 實驗1 整份讀取+批次寫入 
* 每次批次都要清除記憶體 (100w~500w) step = 50w
####  每一百萬筆寫入一次
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 5,000,000 | 20.226/ 8.065 | 488|
| 10,000,000 | 46.984 /15.882   | 495 |
####  每一百五十萬筆寫入一次
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 5,000,000 | 19.272 / 7.529 | 577|
| 10,000,000 | 41.403 /15.016   | 612 |

####  每兩百萬
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 5,000,000 | 17.76 / 7.41 | 1100 |
| 10,000,000 | 39.296 /15.34   | 956 |

| 5,000,000 | 18.153 / 7.866 | 1100 |
| 10,000,000 | 39.043 / 15.595 | 946 |

| 20,000,000 | 39.043 / 15.595 | 946 |
| 40,000,000 | 39.043 / 15.595 | 946 |
| 60,000,000 | 39.043 / 15.595 | 946 |

####  每兩百五十萬 (電腦效能極限)
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 5,000,000 | 16.848 / 7.15 | 1200 |
| 10,000,000 | 37.721 /15.836   | 1300 |

| 5,000,000 | 17.845 /  7.784 | 1200 |
| 10,000,000 | 37.478 / 15.25 | 1200 |

#### 1-2 每三百萬 (電腦效能極限)
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 5,000,000 | 16.945 / 7.559 | 1100 |
| 10,000,000 | 38.56 / 15.446 | 968 |
| 10,000,000 | 32 / 15.446 | 968 |
| 10,000,000 | 20 / 只測讀取| 1600 | => span分片優化+反射優化
| 10,000,000 | 17 / 只測讀取| 1600 | => span分片優化+反射優化+不產生陣列在做反射，直接拿到當下就做反射
| 10,000,000 | 17 / 6.74 共計23.74秒| 1600 | => span分片優化+反射優化+不產生陣列在做反射，直接拿到當下就做反射

#### 1-2 每三百五十萬
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 5,000,000 | 16.933 / 7.594 | 1100 |
| 10,000,000 | 37.05 /  15.469 | 1600 |
#### 1-2 每四百萬
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 5,000,000 | 17.334/ 7.418| 1100 |
| 10,000,000 | 35.53 / 14.625   | 1500 |
#### 1-2 每四百五十萬
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 5,000,000 |  |  |
| 10,000,000 |  |  |
#### 1-2 每五百萬
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 10,000,000 |44.795 / 56.333  | 1500 |

### 實驗2 分批讀取 + 批次寫入
#### 2-1 每10000筆一次
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|



#### 每50000筆一次
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|

#### 每50000筆一次 (GC)
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|

#### 每50000筆一次 (固態硬碟＋GC)
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|


#### 讀取 + 寫入 五條執行緒
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|

#### 讀取 + 寫入 十六條執行緒
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 20,000,000 | 57.173| 2800 |
| 40,000,000 | 117.775 |  |
| 60,000,000 | 177.906 |  |
#### 讀取 + 寫入 每三百萬一次
| 20,000,000 | 59.55 | 2800 |
| 40,000,000 | 112.422 |  |
| 60,000,000 |173.716  |  |




# 記憶體優化

# 單純 Span與 Split 比較

| Method | Mean     | Error   | StdDev  | Gen0   | Allocated |
|------- |---------:|--------:|--------:|-------:|----------:|
| Split  | 215.4 ns | 1.05 ns | 0.88 ns | 0.0916 |     481 B |
| Span   | 288.9 ns | 2.08 ns | 1.95 ns | 0.0477 |     252 B |


# 加上反射後

| Method | Mean     | Error     | StdDev    | Gen0   | Allocated |
|------- |---------:|----------:|----------:|-------:|----------:|
| Split  | 1.512 us | 0.0040 us | 0.0033 us | 0.1507 |     793 B |
| Span   | 1.601 us | 0.0098 us | 0.0081 us | 0.1030 |     541 B |

# 將 property 改為 static 只做一次

| Method | Mean     | Error     | StdDev    | Gen0   | Allocated |
|------- |---------:|----------:|----------:|-------:|----------:|
| Split  | 1.379 us | 0.0055 us | 0.0051 us | 0.1431 |     757 B |
| Span   | 1.446 us | 0.0076 us | 0.0072 us | 0.0954 |     505 B |


# 將 ChangeType拿掉 (字串轉字串記憶體不變)


| Method | Mean     | Error     | StdDev    | Gen0   | Allocated |
|------- |---------:|----------:|----------:|-------:|----------:|
| Split  | 1.175 us | 0.0052 us | 0.0049 us | 0.1431 |     757 B |
| Span   | 1.265 us | 0.0232 us | 0.0206 us | 0.0954 |     505 B |


# 只做一次反射

| Method | Mean     | Error   | StdDev  | Gen0   | Allocated |
|------- |---------:|--------:|--------:|-------:|----------:|
| Split  | 384.9 ns | 3.79 ns | 3.55 ns | 0.1073 |     565 B |
| Span   | 487.2 ns | 6.53 ns | 6.11 ns | 0.0591 |     312 B |


# 只做一次反射 + 不使用陣列暫存讀取過的資料
| Method              | Mean     | Error   | StdDev  | Gen0   | Allocated |
|-------------------- |---------:|--------:|--------:|-------:|----------:|
| ParseAndSetDirectly | 351.5 ns | 1.26 ns | 1.05 ns | 0.0591 |     312 B |
| Span                | 388.6 ns | 2.17 ns | 2.03 ns | 0.0663 |     349 B |

# 只做一次反射 + 不使用陣列暫存讀取過的資料 (.NET8版本)
| Method              | Mean     | Error   | StdDev  | Median   | Gen0   | Allocated |
|-------------------- |---------:|--------:|--------:|---------:|-------:|----------:|
| ParseAndSetDirectly | 113.1 ns | 1.19 ns | 0.93 ns | 113.2 ns | 0.0356 |     448 B |
| Span                | 108.9 ns | 1.76 ns | 3.74 ns | 107.6 ns | 0.0414 |     520 B |




# 200萬筆資料寫入(不優化StringBuilder+Trim)

| Method        | Mean    | Error    | StdDev   | Gen0        | Allocated |
|-------------- |--------:|---------:|---------:|------------:|----------:|
| Origin        | 1.978 s | 0.0277 s | 0.0246 s | 238000.0000 |   1.16 GB |
| StringBuilder | 2.128 s | 0.0069 s | 0.0061 s | 308000.0000 |   1.51 GB |


# 200萬筆資料寫入(優化StringBuilder+Trim)

| Method        | Mean    | Error    | StdDev   | Gen0        | Allocated  |
|-------------- |--------:|---------:|---------:|------------:|-----------:|
| Origin        | 1.748 s | 0.0057 s | 0.0045 s | 224000.0000 | 1123.18 MB |
| StringBuilder | 1.397 s | 0.0120 s | 0.0112 s |  61000.0000 |  305.63 MB |

# 200萬筆資料寫入(優化StringBuilder+Trim+反射)

| Method        | Mean    | Error    | StdDev   | Gen0        | Allocated  |
|-------------- |--------:|---------:|---------:|------------:|-----------:|
| Origin        | 1.748 s | 0.0057 s | 0.0045 s | 224000.0000 | 1123.18 MB |
| StringBuilder | 1.397 s | 0.0120 s | 0.0112 s |  61000.0000 |  305.63 MB |



| Method   | Mean     | Error   | StdDev  | Gen0       | Allocated    |
|--------- |---------:|--------:|--------:|-----------:|-------------:|
| Read     | 127.1 ms | 0.45 ms | 0.42 ms |          - |         8 KB |
| ReadLine | 170.3 ms | 2.75 ms | 2.58 ms | 29333.3333 | 150519.25 KB |



--
## Parallel 平行處理總優化版本:
### 對照組(原本優化後但不使用Parallel)
#### 讀取 + 寫入 十六條執行緒
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 20,000,000 | 57.173| 2800 |
| 40,000,000 | 117.775 |  |
| 60,000,000 | 177.906 |  |
#### 讀取 + 寫入 每三百萬一次
| 筆數 | 秒數(s) | 記憶體用量(mb) |
|------|-----|------------|
| 20,000,000 | 59.55 | 2800 |
| 40,000,000 | 112.422 |  |
| 60,000,000 |173.716  |  |

####  每三百萬 
| 筆數 | 平均讀取秒數(s) / 平均寫入秒數(s) | 總執行時間(s) |  記憶體用量(mb) |
|------|-----|----|-----------|
| 10,000,000 | 12.317 / 1.181 | 18.7s |2600 |
| 20,000,000 | 14.79 / 6.3 | 37.52s |1700 |
| 40,000,000 | 17.5 / 5.445 | 76.08s |1900 |
| 60,000,000 | 20.2 / 2.68 | 109.08s |1700 |