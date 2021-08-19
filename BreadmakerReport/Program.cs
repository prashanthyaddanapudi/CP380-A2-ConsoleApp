using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RatingAdjustment.Services;
using BreadmakerReport.Models;
using System.Collections.Generic;

namespace BreadmakerReport
{
    class Program
    {

        static void Main(string[] args)
        {

            string database = @".\data\breadmakers.db";
            RatingAdjustmentService rt= new RatingAdjustmentService();
           
            var BreadmakerDb = new BreadMakerSqliteContext(database);
            var breakMakersList = BreadmakerDb.Breadmakers.Select(breadmaker => new {
                breadmakerdata = breadmaker,
                Reviews=breadmaker.Reviews.ToList()
            }).AsEnumerable().ToList();
            var result =new  List<Result>();
            foreach (var maker in breakMakersList)
            {
                var review = maker.breadmakerdata.Reviews.Count;
                var average = maker.Reviews.Average(review => review.stars);
                var adjust = (rt.Adjustdata(average, review));
                var title = maker.breadmakerdata.title;

                result.Add(
                    new Result
                    {
                        reviews = review,
                        average = average,
                        adjust = adjust,
                        title = title,
                    }
                );

            }

                var check = 0;
                Console.WriteLine("Welcome to Bread World");

                Console.WriteLine("     Reviews Average Adjust       Title");

                foreach (var i in result.AsEnumerable().OrderByDescending(bm => bm.adjust))
                {
                    if ((check++) < 3)
                        Console.WriteLine($"{check} {i.reviews}     { i.average} { i.adjust}   {i.title}");
                    else
                        break;

                }
            

        }

    }


    class Result
    {
        public double reviews;
        public double average;
        public double adjust;
        public String title;
    }
}
