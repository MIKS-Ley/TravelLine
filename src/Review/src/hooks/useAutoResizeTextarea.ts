import { useCallback } from 'react';

export const useAutoResizeTextarea = () => {
  const autoResize = useCallback((textarea: HTMLTextAreaElement) => {
    textarea.style.height = 'auto';
    textarea.style.height = `${textarea.scrollHeight}px`;
  }, []);

  return autoResize;
};