using System;

namespace UserStory4Tours.models
{
    /// <summary>
    /// Горящие туры(все что они включают)
    /// </summary>
    public class Tours
    {   /// <summary>
        /// Первичный ключ
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Направление
        /// </summary>
        public Direction direction { get; set; }
        /// <summary>
        /// Дату вылета
        /// </summary>
        public DateTime  DateDeparture{ get; set; }
        /// <summary>
        /// Количество ночей
        /// </summary>
        public int NumberNight { get; set; }
        /// <summary>
        /// Стоимость за отдыхающего(руб).
        /// </summary>
        public decimal CostVac { get; set; }
        /// <summary>
        /// Количество отдыхающих
        /// </summary>
        public int NumberVac { get; set; }
        /// <summary>
        /// Наличие Wi-Fi
        /// </summary>
        public bool Wi_Fi { get; set; }
        /// <summary>
        /// Доплаты(руб).
        /// </summary>
        public decimal Surcharges { get; set; }
        /// <summary>
        /// Проверка миграции
        /// </summary>
        public string MigrationVerification { get; set; }

    }
}
