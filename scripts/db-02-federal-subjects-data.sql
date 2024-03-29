--

insert into federal_subjects.federal_districts (id, name)
values
    (1, 'Центральный федеральный округ'),
    (2, 'Северо-Западный федеральный округ'),
    (3, 'Южный федеральный округ'),
    (4, 'Северо-Кавказский федеральный округ'),
    (5, 'Приволжский федеральный округ'),
    (6, 'Уральский федеральный округ'),
    (7, 'Сибирский федеральный округ'),
    (8, 'Дальневосточный федеральный округ');

insert into federal_subjects.federal_subjects_types (id, name)
values
    (1, 'Республика'),
    (2, 'Край'),
    (3, 'Область'),
    (4, 'Город федерального значения'),
    (5, 'Автономная область'),
    (6, 'Автономный округ');

insert into federal_subjects.federal_subjects (id, federal_district_id, federal_subject_type_id, name)
values
    (1, 2, 3, 'Мурманская область'),
    (2, 2, 1, 'Республика Карелия'),
    (3, 2, 3, 'Архангельская область'),
    (4, 2, 6, 'Ненецкий автономный округ'),
    (5, 2, 3, 'Вологодская область'),
    (6, 2, 1, 'Республика Коми');

--
