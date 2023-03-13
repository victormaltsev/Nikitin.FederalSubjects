--

create schema if not exists federal_subjects;

--

create table if not exists federal_subjects.federal_districts (
    id smallserial primary key,
    name varchar(128) not null,

    unique (name) -- also unique index
);

create table if not exists federal_subjects.federal_subjects_types (
    id smallserial primary key,
    name varchar(128) not null,

    unique (name) -- also unique index
);

create table if not exists federal_subjects.federal_subjects (
    id smallserial primary key,
    federal_district_id smallint not null,
    federal_subject_type_id smallint not null,
    name varchar(128) not null,
    description text,
    content text,

    unique (name), -- also unique index

    foreign key (federal_district_id) references federal_subjects.federal_districts (id),
    foreign key (federal_subject_type_id) references federal_subjects.federal_subjects_types (id)
);

--
