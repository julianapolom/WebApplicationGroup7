declare @employees as table (emp_no int, first_name varchar(10), last_name varchar(10))
declare @salaries as table (emp_no int, salary int)

insert into @employees
values(1, 'Pepe', 'Alzate'),
(1, 'Pepe', 'Alzate'),
(2, 'Pepe', 'Alzate'),
(3, 'Pepe', 'Alzate')

insert into @salaries
values (1, 100000),
(2, 999999),
(3, 777777)

SELECT first_name +' '+ last_name AS employee, salary
FROM employees e
INNER JOIN salaries s ON s.emp_no = e.emp_no
WHERE salary = (SELECT MAX(salary) FROM @salaries)