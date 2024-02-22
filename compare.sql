SELECT worker_id
FROM
(SELECT worker_id FROM workers
UNION ALL  
SELECT worker_id FROM worker_path) data
GROUP BY worker_id








