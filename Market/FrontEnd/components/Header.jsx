import { useState, useEffect } from 'react';
import MetallicPaint, { parseLogoImage } from '../MetallicPaint/MetallicPaint';
import logo from '../src/assets/logos/logo.svg'

export default function Header() {
  const [imageData, setImageData] = useState(null);

  useEffect(() => {
    // Загружаем логотип из файла или URL и преобразуем в ImageData
    async function loadImage() {
      const response = await fetch(logo); // заменяй на путь к картинке
      const blob = await response.blob();

      try {
        const result = await parseLogoImage(blob);
        setImageData(result.imageData);
      } catch (e) {
        console.error('Failed to parse image:', e);
      }
    }
    loadImage();
  }, []);

  return (
    <header className="header">
      <div className="logo">
        {imageData ? (
          <MetallicPaint imageData={imageData} />
        ) : (
          <p>Loading animation...</p>
        )}
      </div>
    </header>
  );
}
