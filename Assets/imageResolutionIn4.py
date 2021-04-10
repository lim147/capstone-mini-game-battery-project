import os
from PIL import Image

# The image textures in Unity need to be in multiples of 4 to allow compression
# This simple script resizes all images ending .jpg, .jpeg, or .png within its directory
# so that their width and height are rounded to the nearest multiple of 4.
# To run, you likely need to `pip3 install Image`, then do `python3 imageResolutionIn4.py`.
for root, dirs, files in os.walk("."):
    for filename in files:
        fullpath = os.path.join(root, filename)
        if fullpath.endswith(".jpg") or fullpath.endswith(".jpeg") or fullpath.endswith(".png"):
            image = Image.open(fullpath)
            newWidth = image.width // 4 * 4
            newHeight = image.height // 4 * 4
            resizedImage = image.resize(size=(newWidth, newHeight))
            resizedImage.save(fullpath)
            print(fullpath, f"width: {image.width}",
                  f"height: {image.height}", "->", f"width: {newWidth}", f"height: {newHeight}")
