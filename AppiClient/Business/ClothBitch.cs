﻿using CitizenFX.Core;

namespace Client.Business
{
    public class ClothBitch : BaseScript
    {
        public static dynamic[,] TshirtM =
        {
            { 11, 5, 2, 20, 5, "Майка-1" },
            { 11, 17, 5, 20, 5, "Майка-2" },
            { 11, 36, 5, 22, 5, "Майка-3" },
            { 11, 0, 5, 15, 2, "Футболка-1" },
            { 11, 1, 1, 15, 2, "Футболка-2" },
            { 11, 16, 2, 15, 2, "Футболка-3" },
            { 11, 22, 2, 15, 2, "Футболка-4" },
            { 11, 33, 0, 17, 2, "Футболка-5" },
            { 11, 34, 1, 15, 2, "Футболка-6" },
            { 11, 44, 3, 15, 2, "Футболка-7" },
            { 11, 80, 2, 25, 2, "Футболка-8" },
            { 11, 81, 2, 25, 2, "Футболка-9" },
            { 11, 93, 2, 20, 2, "Футболка-10" },
            { 11, 94, 2, 20, 2, "Футболка-11" },
            { 11, 97, 1, 15, 2, "Футболка-12" },
            { 11, 123, 2, 30, 2, "Футболка-13" },
            { 11, 128, 9, 45, 2, "Футболка-14" },
            { 11, 131, 0, 20, 2, "Футболка-15" },
            { 11, 164, 2, 35, 2, "Футболка-16" }
        };
        
        public static dynamic[,] TshirtF =
        {
            { 11, 23, 2, 25, 0, "Футболка-1" },
            { 11, 49, 1, 25, 7, "Футболка-2" },
            { 11, 73, 2, 26, 7, "Футболка-3" },
            { 11, 209, 16, 32, 0, "Футболка-4" },
            { 11, 169, 5, 29, 0, "Футболка-5" },
            { 11, 168, 5, 25, 0, "Футболка-6" },
            { 11, 141, 5, 17, 7, "Футболка-7" },
            { 11, 126, 2, 35, 7, "Футболка-8" },
            { 11, 125, 9, 35, 7, "Футболка-9" },
            { 11, 5, 1, 20, 0, "Топик-1" },
            { 11, 33, 8, 21, 0, "Топик-2" },
            { 11, 74, 2, 24, 0, "Топик-3" },
            { 11, 170, 5, 19, 0, "Топик-4" },
            { 11, 11, 2, 22, 0, "Майка-1" },
            { 11, 36, 4, 30, 0, "Майка-2" },
            { 11, 118, 2, 21, 0, "Майка-3" }
        };
        
        public static dynamic[,] BlouseM =
        {
            { 11, 13, 3, 40, 4, "Рубашка-1" },
            { 11, 14, 15, 30, 3, "Рубашка-2" },
            { 11, 26, 9, 40, 4, "Рубашка-3" },
            { 11, 41, 3, 25, 3, "Рубашка-4" },
            { 11, 63, 0, 22, 2, "Рубашка-5" },
            { 11, 117, 15, 25, 7, "Рубашка-6" },
            { 11, 126, 14, 30, 3, "Рубашка-7" },
            { 11, 127, 14, 30, 1, "Рубашка-8" },
            { 11, 133, 0, 40, 4, "Рубашка-9" },
            { 11, 84, 5, 30, 3, "Кофта-1" },
            { 11, 111, 5, 50, 6, "Кофта-2" },
            { 11, 57, 0, 45, 3, "Толстовка-1" },
            { 11, 86, 4, 49, 3, "Толстовка-2" },
            { 11, 96, 0, 40, 3, "Толстовка-3" },
            { 11, 121, 11, 50, 3, "Толстовка-4" },
            { 11, 134, 2, 40, 3, "Толстовка-5" },
            { 11, 171, 1, 50, 3, "Толстовка-6" },
            { 11, 202, 4, 50, 0, "Толстовка-7" },
            { 11, 205, 4, 50, 0, "Толстовка-8" }
        };
        
        public static dynamic[,] BlouseF =
        {
            { 11, 45, 3, 35, 3, "Кофта-1" },
            { 11, 50, 0, 42, 3, "Кофта-2" },
            { 11, 61, 3, 29, 3, "Кофта-3" },
            { 11, 207, 4, 30, 0, "Кофта-4" },
            { 11, 199, 15, 45, 1, "Кофта-5" },
            { 11, 136, 7, 40, 3, "Кофта-6" },
            { 11, 103, 5, 40, 3, "Кофта-7" },
            { 11, 9, 14, 40, 2, "Рубашка-1" },
            { 11, 17, 0, 40, 2, "Рубашка-2" },
            { 11, 56, 0, 38, 7, "Рубашка-3" },
            { 11, 76, 4, 31, 10, "Рубашка-4" },
            { 11, 121, 16, 40, 8, "Рубашка-5" },
            { 11, 120, 16, 40, 8, "Рубашка-6" },
            { 11, 109, 15, 37, 2, "Рубашка-7" },
            { 11, 83, 6, 39, 1, "Рубашка-8" },
            { 11, 3, 4, 30, 3, "Толстовка-1" },
            { 11, 184, 1, 50, 3, "Толстовка-2" },
            { 11, 156, 1, 36, 0, "Жилетка-1" },
            { 11, 201, 7, 41, 8, "Свитер-1" }
        };
        
        public static dynamic[,] JacketM =
        {
            { 11, 3, 15, 25, 8, "Куртка-1" },
            { 11, 6, 1, 30, 8, "Куртка-2" },
            { 11, 37, 2, 60, 8, "Куртка-3" },
            { 11, 61, 3, 40, 3, "Куртка-4" },
            { 11, 62, 0, 60, 8, "куртка-5" },
            { 11, 64, 0, 60, 8, "куртка-6" },
            { 11, 65, 3, 30, 1, "Куртка-7" },
            { 11, 85, 0, 35, 3, "Куртка-8" },
            { 11, 87, 11, 50, 3, "Куртка-9" },
            { 11, 88, 11, 50, 8, "Куртка-10" },
            { 11, 90, 0, 55, 3, "Куртка-11" },
            { 11, 106, 0, 45, 8, "Куртка-12" },
            { 11, 113, 3, 34, 7, "Куртка-13" },
            { 11, 118, 9, 60, 8, "Куртка-14" },
            { 11, 122, 13, 59, 8, "Куртка-15" },
            { 11, 136, 6, 45, 8, "Куртка-16" },
            { 11, 141, 10, 35, 7, "Куртка-17" },
            { 11, 149, 9, 40, 1, "Куртка-18" },
            { 11, 150, 11, 50, 3, "Куртка-19" },
            { 11, 153, 25, 37, 3, "Куртка-20" },
            { 11, 161, 3, 55, 8, "Куртка-21" },
            { 11, 168, 2, 42, 7, "Куртка-22" },
            { 11, 169, 3, 40, 8, "Куртка-23" }
        };
        
        public static dynamic[,] JacketF =
        {
            { 11, 165, 2, 40, 1, "Куртка-1" },
            { 11, 163, 5, 50, 4, "Куртка-2" },
            { 11, 162, 6, 45, 1, "Куртка-3" },
            { 11, 150, 25, 30, 1, "Куртка-4" },
            { 11, 144, 9, 50, 3, "Куртка-5" },
            { 11, 133, 6, 42, 4, "Куртка-6" },
            { 11, 110, 9, 40, 3, "Куртка-7" },
            { 11, 106, 3, 34, 8, "Куртка-8" },
            { 11, 80, 0, 51, 3, "Куртка-9" },
            { 11, 77, 0, 49, 8, "Куртка-10" },
            { 11, 55, 0, 42, 3, "Куртка-11" },
            { 11, 54, 3, 48, 3, "Куртка-12" },
            { 11, 166, 3, 40, 5, "Куртка-13" },
            { 11, 189, 12, 78, 8, "Парка-1" },
            { 11, 157, 1, 35, 6, "Жилетка-1" },
            { 11, 35, 11, 55, 5, "Жилетка-2" },
            { 11, 1, 2, 53, 5, "Жилетка-3" },
            { 11, 167, 3, 54, 0, "Жилетка-4" }
        };
        
        public static dynamic[,] OffJacketM =
        {
            { 11, 19, 1, 80, 13, "Пиджак-1" },
            { 11, 109, 0, 50, 9, "Жилетка-1" },
            { 11, 137, 2, 50, 9, "Жилетка-2" },
            { 11, 40, 1, 80, 9, "Жилетка-3" }
        };
        
        public static dynamic[,] OffJacketF =
        {
            { 11, 20, 1, 70, 5, "Пиджак" }
        };
        
        public static dynamic[,] PantsM =
        {
            { 4, 0, 15, 35, 0, "Джинсы-1" },
            { 4, 1, 15, 35, 0, "Джинсы-2" },
            { 4, 43, 1, 33, 0, "Джинсы-3" },
            { 4, 63, 0, 50, 0, "Джинсы-4" },
            { 4, 75, 7, 34, 0, "Джинсы-5" },
            { 4, 76, 7, 39, 0, "Джинсы-6" },
            { 4, 82, 9, 70, 0, "Джинсы-7" },
            { 4, 3, 15, 25, 0, "Штаны-1" },
            { 4, 5, 15, 20, 0, "Штаны-2" },
            { 4, 7, 14, 40, 0, "Штаны-3" },
            { 4, 8, 0, 50, 0, "Штаны-4" },
            { 4, 9, 15, 80, 0, "Штаны-5" },
            { 4, 25, 6, 90, 0, "Штаны-6" },
            { 4, 27, 11, 60, 0, "Штаны-7" },
            { 4, 36, 0, 10, 0, "Штаны-8" },
            { 4, 47, 1, 40, 0, "Штаны-9" },
            { 4, 55, 3, 22, 0, "Штаны-10" },
            { 4, 64, 10, 22, 0, "Штаны-11" },
            { 4, 68, 9, 105, 0, "Штаны-12" },
            { 4, 69, 17, 40, 0, "Штаны-13" },
            { 4, 70, 3, 105, 0, "Штаны-14" },
            { 4, 78, 7, 60, 0, "Штаны-15" },
            { 4, 32, 3, 30, 0, "Подштанники-1" },
            { 4, 17, 10, 25, 0, "Шорты-1" },
            { 4, 42, 7, 20, 0, "Шорты-2" },
            { 4, 62, 3, 15, 0, "Шорты-3" }
        };
        
        public static dynamic[,] PantsF =
        {
            { 4, 0, 15, 50, 1, "Джинсы-1" },
            { 4, 1, 15, 30, 1, "Джинсы-2" },
            { 4, 11, 15, 45, 1, "Джинсы-3" },
            { 4, 51, 4, 35, 1, "Джинсы-4" },
            { 4, 84, 9, 55, 1, "Джинсы-5" },
            { 4, 2, 2, 30, 1, "Штаны-1" },
            { 4, 3, 14, 25, 1, "Штаны-2" },
            { 4, 31, 3, 32, 1, "Штаны-3" },
            { 4, 34, 0, 55, 1, "Штаны-4" },
            { 4, 35, 0, 10, 1, "Штаны-5" },
            { 4, 45, 3, 52, 1, "Штаны-6" },
            { 4, 47, 6, 40, 1, "Штаны-7" },
            { 4, 49, 1, 45, 1, "Штаны-8" },
            { 4, 52, 3, 60, 1, "Штаны-9" },
            { 4, 54, 3, 48, 1, "Штаны-10" },
            { 4, 58, 3, 15, 1, "Штаны-11" },
            { 4, 64, 3, 62, 1, "Штаны-12" },
            { 4, 66, 10, 15, 1, "Штаны-13" },
            { 4, 67, 13, 38, 1, "Штаны-14" },
            { 4, 71, 17, 23, 1, "Штаны-15" },
            { 4, 80, 6, 41, 1, "Штаны-16" },
            { 4, 82, 7, 31, 1, "Штаны-17" },
            { 4, 87, 15, 35, 1, "Штаны-18" },
            { 4, 9, 15, 35, 1, "Юбка-1" },
            { 4, 10, 2, 15, 1, "Шорты-1" }
        };
        
        public static dynamic[,] ShortsM =
        {
            { 4, 61, 13, 5, 0, "Трусы" }
        };
        
        public static dynamic[,] ShortsF =
        {
            { 11, 18, 11, 12, 0, "Лифчик" },
            { 4, 17, 11, 12, 0, "Трусы-1" },
            { 4, 19, 4, 14, 1, "Трусы-2" }
        };
        
        public static dynamic[,] FootwearM =
        {
            { 6, 5, 3, 10, 0, "Тапки-1" },
            { 6, 6, 1, 20, 0, "Тапки-2" },
            { 6, 16, 11, 20, 0, "Тапки-3" },
            { 6, 1, 15, 50, 0, "Кроссовки-1" },
            { 6, 4, 2, 40, 0, "Кроссовки-2" },
            { 6, 8, 15, 45, 0, "Кроссовки-3" },
            { 6, 9, 15, 55, 0, "Кроссовки-4" },
            { 6, 22, 11, 40, 0, "Кроссовки-5" },
            { 6, 26, 15, 45, 0, "Кроссовки-6" },
            { 6, 28, 5, 80, 0, "Кроссовки-7" },
            { 6, 31, 4, 99, 0, "Кроссовки-8" },
            { 6, 32, 15, 70, 0, "Кроссовки-9" },
            { 6, 42, 9, 50, 0, "Кроссовки-10" },
            { 6, 46, 9, 49, 0, "Кроссовки-11" },
            { 6, 48, 1, 40, 0, "Кроссовки-12" },
            { 6, 57, 11, 95, 0, "Кроссовки-13" },
            { 6, 43, 7, 60, 0, "Ботинки-1" },
            { 6, 45, 10, 90, 0, "Ботинки-2" },
            { 6, 35, 1, 120, 0, "Ботинки-3" },
            { 6, 41, 0, 70, 0, "Ботинки-4" }
        };
        
        public static dynamic[,] FootwearF =
        {
            { 6, 2, 15, 50, 1, "Угги-1" },
            { 6, 0, 3, 60, 1, "Туфли-1" },
            { 6, 19, 11, 64, 1, "Туфли-2" },
            { 6, 37, 3, 70, 1, "Туфли-3" },
            { 6, 41, 11, 50, 1, "Туфли-4" },
            { 6, 13, 15, 25, 1, "балетки-1" },
            { 6, 1, 15, 35, 1, "Кроссовки-1" },
            { 6, 3, 15, 20, 1, "Кроссовки-2" },
            { 6, 10, 3, 30, 1, "Кроссовки-3" },
            { 6, 27, 0, 26, 1, "Кроссовки-4" },
            { 6, 28, 0, 30, 1, "Кроссовки-5" },
            { 6, 33, 7, 25, 1, "Кроссовки-6" },
            { 6, 47, 9, 32, 1, "Кроссовки-7" },
            { 6, 49, 1, 20, 1, "Кроссовки-8" },
            { 6, 50, 1, 20, 1, "Кроссовки-9" },
            { 6, 5, 1, 10, 1, "Тапки-1" },
            { 6, 15, 15, 23, 1, "Тапки-2" },
            { 6, 16, 11, 16, 1, "Тапки-3" },
            { 6, 24, 0, 40, 1, "Ботинки-1" },
            { 6, 25, 0, 70, 1, "Ботинки-2" },
            { 6, 26, 0, 13, 1, "Ботинки-3" },
            { 6, 36, 0, 80, 1, "Ботинки-4" },
            { 6, 48, 11, 99, 1, "Ботинки-5" },
            { 6, 52, 5, 40, 1, "Ботинки-6" },
            { 6, 53, 1, 60, 1, "Ботинки-7" },
            { 6, 55, 5, 45, 1, "Ботинки-8" },
            { 6, 57, 2, 50, 1, "Ботинки-9" },
            { 6, 59, 1, 52, 1, "Ботинки-10" },
            { 6, 43, 7, 72, 1, "Сапоги-1" },
            { 6, 51, 5, 82, 1, "Сапоги-2" },
            { 6, 54, 5, 85, 1, "Сапоги-3" },
            { 6, 56, 2, 95, 1, "Сапоги-4" }
        };
    }
}