import json

# Generate base message map JSON file from messages.ini

class MessageMapItem(object):
    Name: str
    Header: int
    Hash: str
    Aliases: list[str]

map = {
    'Incoming': [],
    'Outgoing': []
}

with open('messages.ini', 'r') as f:
    section = None
    for line in f.readlines():
        line = line.strip()
        if len(line) == 0: continue
        if line[0] == ';': continue
        if line[0] == '[' and line[-1] == ']':
            section = line[1:-1].strip()
            continue
        if '=' not in line: continue
        if section not in map: continue
        index = line.index('=')
        key = line[:index].strip()
        value = line[index+1:].strip()
        item = MessageMapItem()
        item.Name = key
        item.Header = int(value)
        item.Hash = ''
        item.Aliases = []
        map[section].append(item)

with open('messages.json', 'w') as f:
    f.write(json.dumps(map, indent = 2, default = lambda x: x.__dict__))