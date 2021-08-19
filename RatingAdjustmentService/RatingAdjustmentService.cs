using System;

namespace RatingAdjustment.Services
{
    /** Service calculating a star rating accounting for the number of reviews
     * 
     */
    public class RatingAdjustmentService
    {
        
        double calc_val;
        double positive_percent;
        const double MAX_STARS = 5.0;  // Likert scale
        const double _check = 1.96; // 95% confidence interval

        /** Percentage of positive reviews
         * 
         * In this case, that means X of 5 ==> percent positive
         * 
         * Returns: [0, 1]
         */
        void SetPercentPositive(double stars)
        {
            // TODO: Implement this!
            positive_percent = stars / MAX_STARS;

        }

        /**
         * Calculate "Q" given the formula in the problem statement
         */
        void SetQ(double number_of_ratings)
        {
            // TODO: Implement this!
            double square = _check * _check;
            double caluclated_val = positive_percent * (1 - positive_percent);
            caluclated_val = (caluclated_val+ square / (4 * number_of_ratings));
            caluclated_val = caluclated_val/ number_of_ratings;
            caluclated_val = _check * Math.Sqrt(caluclated_val);
            calc_val = caluclated_val;
        }

        /** Adjusted lower bound
         * 
         * Lower bound of the confidence interval around the star rating.
         * 
         * Returns: a double, up to 5
         */
        public double Adjustdata(double stars, double number_of_ratings)
        {
            // TODO: Implement this!
            SetPercentPositive(stars);
            SetQ(number_of_ratings);
            double square = _check * _check;
            double calc = positive_percent - calc_val;
            calc = calc +( square / (2 * number_of_ratings));
            calc = (calc / 1 + square / number_of_ratings);
            double lowerBound =( calc * MAX_STARS);
            return lowerBound;
        }
    }
}
