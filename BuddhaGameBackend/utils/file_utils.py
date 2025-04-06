import csv
import hashlib
import json
import os
from pathlib import Path
from urllib.parse import quote, unquote
import chardet


def read_txt(file):
    with open(file, "r", encoding="utf8") as f:
        return f.read()


def write_txt(file, data):
    with open(file, "w", encoding="utf8") as f:
        f.write(data)


def read_json(file):
    return json.loads(read_txt(file))


def write_json(file, data):
    write_txt(file, json.dumps(data))


def read_txt_lines(file):
    content = read_txt(file)
    return content.strip().split("\n")


def write_txt_lines(file, lines):
    with open(file, "w", encoding="utf8") as f:
        for l in lines:
            f.writelines(l + "\n")


def read_txt_chardet(filename):
    with open(str(filename), mode="rb") as f:
        result = chardet.detect(f.read())
        # if not str(result["encoding"]).lower().startswith("utf-8"):
        #     print("文件 {} 格式:{}".format(filename, result))
        encoding = result["encoding"]
    with open(str(filename), mode="r", encoding=encoding) as f:
        return f.read()


def read_csv(file):
    with open(file, encoding="utf-8-sig") as f:
        return list(csv.DictReader(f))


def md5(s):
    return hashlib.md5(s.encode('utf8')).hexdigest()


def url_encode(s):
    return quote(s)


def url_decode(s):
    return unquote(s)


def getFoldersFilesRecursion(folder_list, extends=None):
    if extends is None:
        extends = ['srt', 'mkv', 'mp4', "flv"]
    files_list = []
    for folder in folder_list:
        path = [p for ext in extends for p in Path(f'{folder}').glob(f'**/*.{ext}')]
        for p in path:
            files_list.append(p)
    return files_list


def create_folder(path):
    path = Path(str(path))
    if path.is_file():
        if not path.parent.exists():
            os.makedirs(str(path.parent))
    else:
        if not path.exists():
            if path.suffix != "":
                if not path.parent.exists():
                    os.makedirs(str(path.parent))
            else:
                os.makedirs(str(path))
