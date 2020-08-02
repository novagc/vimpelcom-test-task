CREATE TABLE res_table AS
WITH 
odt AS (SELECT dt as f_date, ROW_NUMBER() OVER(ORDER BY dt ASC) AS _id FROM md),
sodt AS (SELECT f_date AS s_date, _id FROM odt OFFSET 1)
SELECT f_date, s_date FROM odt LEFT JOIN sodt ON sodt._id - odt._id = 1;