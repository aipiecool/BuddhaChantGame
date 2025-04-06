import datetime
import time


def string_to_timestamp(date_string):
    if isinstance(date_string, datetime.datetime):
        return int(date_string.timestamp())
    if date_string == '0000-00-00 00:00:00':
        return 0
    timestamp = int(time.mktime(time.strptime(date_string, '%Y-%m-%d %H:%M:%S')))
    return timestamp


def timestamp_to_day_string(t):
    if isinstance(t, int):
        t = datetime.datetime.fromtimestamp(t)
    return t.strftime('%Y-%m-%d')


def timestamp_to_daytime_string(t):
    if isinstance(t, int):
        t = datetime.datetime.fromtimestamp(t)
    return t.strftime('%Y-%m-%d %H:%M:%S')


def sec_to_day(sec):
    day = sec // (60 * 60 * 24)
    return day


def day_to_sec(day):
    sec = day * (60 * 60 * 24)
    return sec


def day_of_month():
    t = datetime.datetime.now(UTC(8))
    return t.day


def now_string():
    t = datetime.datetime.now(UTC(8))
    return t.strftime('%Y-%m-%d %H:%M:%S')


def now_sec():
    t = datetime.datetime.now(UTC(8))
    return int(t.timestamp())


def day_now_string():
    t = datetime.datetime.now(UTC(8))
    return t.strftime('%Y-%m-%d')


def yesterday_string():
    yesterday = datetime.datetime.now(UTC(8)) - datetime.timedelta(days=1)
    return yesterday.strftime('%Y-%m-%d')


def string_time_day_sub(date_string1, date_string2):
    t1 = string_to_timestamp(date_string1)
    t2 = string_to_timestamp(date_string2)
    sec = t1 - t2
    day = sec // (60 * 60 * 24) + 1
    return day


class UTC(datetime.tzinfo):

    def __init__(self, offset=0):
        self._offset = offset

    def utcoffset(self, dt):
        return datetime.timedelta(hours=self._offset)

    def tzname(self, dt):
        return "UTC +%s" % self._offset

    def dst(self, dt):
        return datetime.timedelta(hours=self._offset)
