from sqlalchemy import text, desc
from ext import db


def db_add(entity):
    db.session.add(entity)
    db.session.commit()
    return entity


def db_query_first(clazz, params):
    data = clazz.query.filter_by(**params).first()
    return data


def db_query_all(clazz, params):
    data = clazz.query.filter_by(**params).order_by(desc('ID')).all()
    return data


def db_query_page(clazz, params, page, page_count=10):
    data = clazz.query.filter_by(**params).order_by(desc('ID')).offset(page*page_count).limit(page_count).all()
    return data


def db_count(clazz, params):
    data = clazz.query.filter_by(**params).count()
    return data


def db_sql(sql_str):
    return db.session.execute(text(sql_str))
