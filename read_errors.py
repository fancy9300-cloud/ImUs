try:
    with open('build_log.txt', 'r', encoding='utf-16') as f:
        content = f.read()
        print(content)
except Exception as e:
    print(f"Failed to read file: {e}")
