import React, { useState, useRef, useCallback, useEffect } from "react";
import type { Day, DayId, Task } from './types/taskTypes';
import DayColumn from './components/DayColumn/DayColumn';
import DragPreview from './components/DragPreview/DragPreview';
import './App.css';

const App: React.FC = () => {
  const initialDays: Day[] = [
    { id: 'monday', title: 'Понедельник' },
    { id: 'tuesday', title: 'Вторник' },
    { id: 'wednesday', title: 'Среда' },
    { id: 'thursday', title: 'Четверг' },
  ];

  const initialTasks: Task[] = [
    { id: '1', content: 'Домашнее задание', dayId: 'monday' },
    { id: '2', content: 'Помыть посуду', dayId: 'tuesday' },
    { id: '3', content: 'Сделать зарядку', dayId: 'wednesday' },
    { id: '4', content: 'Прогуляться', dayId: 'monday' },
    { id: '5', content: 'Прочитать книгу', dayId: 'thursday' },
  ];

  const [tasks, setTasks] = useState<Task[]>(initialTasks);
  const [days] = useState<Day[]>(initialDays);
  const [dragState, setDragState] = useState<{
    task: Task;
    offsetX: number;
    offsetY: number;
  } | null>(null);
  const [previewPos, setPreviewPos] = useState({ x: 0, y: 0 });
  const [dropTarget, setDropTarget] = useState<{
    dayId: DayId;
    index: number;
  } | null>(null);

  const columnRefs = useRef<{ [key: string]: HTMLDivElement | null }>({});
  const taskRefs = useRef<{ [key: string]: HTMLDivElement | null }>({});

  const handleDragStart = useCallback(
    (task: Task, clientX: number, clientY: number, offsetX: number, offsetY: number) => {
      setDragState({ task, offsetX, offsetY });
      setPreviewPos({ x: clientX - offsetX, y: clientY - offsetY });
    },
    []
  );

const handleMouseMove = useCallback(
  (e: MouseEvent) => {
    if (!dragState) return;

    setPreviewPos({
      x: e.clientX - dragState.offsetX,
      y: e.clientY - dragState.offsetY,
    });

    let targetDayId: DayId | null = null;
    days.forEach((day) => {
      const ref = columnRefs.current[day.id];
      if (!ref) return;
      const rect = ref.getBoundingClientRect();
      if (
        e.clientX >= rect.left &&
        e.clientX <= rect.right &&
        e.clientY >= rect.top &&
        e.clientY <= rect.bottom
      ) {
        targetDayId = day.id;
      }
    });

    if (!targetDayId) {
      setDropTarget(null);
      return;
    }

    const targetTasks = tasks.filter(t => t.dayId === targetDayId);
    let insertionIndex = targetTasks.length;

    // Если список пустой, вставляем в начало
    if (targetTasks.length === 0) {
      setDropTarget({ dayId: targetDayId!, index: 0 });
      return;
    }

    // Проверяем позицию относительно каждой задачи
    for (let i = 0; i < targetTasks.length; i++) {
      const task = targetTasks[i];
      const taskElement = taskRefs.current[task.id];
      if (!taskElement) continue;
      const taskRect = taskElement.getBoundingClientRect();
      const taskCenterY = taskRect.top + taskRect.height / 2;
      
      if (e.clientY < taskCenterY) {
        insertionIndex = i;
        break;
      }
    }

    setDropTarget({ dayId: targetDayId!, index: insertionIndex });
  },
  [dragState, days, tasks]
);

  const handleMouseUp = useCallback(() => {
    if (!dragState || !dropTarget) {
      setDragState(null);
      setDropTarget(null);
      return;
    }

    setTasks(prevTasks => {
      const newTasks = [...prevTasks];
      const taskIndex = newTasks.findIndex(t => t.id === dragState.task.id);
      const [movedTask] = newTasks.splice(taskIndex, 1);
      movedTask.dayId = dropTarget.dayId;
      
      const targetTasks = newTasks.filter(t => t.dayId === dropTarget.dayId);
      let insertPos = 0;
      for (let i = 0; i < newTasks.length; i++) {
        if (newTasks[i].dayId === dropTarget.dayId) {
          if (insertPos === dropTarget.index) {
            newTasks.splice(i, 0, movedTask);
            return newTasks;
          }
          insertPos++;
        }
      }
      
      newTasks.push(movedTask);
      return newTasks;
    });

    setDragState(null);
    setDropTarget(null);
  }, [dragState, dropTarget]);

useEffect(() => {
  const preventSelection = (e: Event) => {
    if (dragState) {
      e.preventDefault();
    }
  };

  if (dragState) {
    window.addEventListener('mousemove', handleMouseMove);
    window.addEventListener('mouseup', handleMouseUp);
    document.addEventListener('selectstart', preventSelection);
  }
  
  return () => {
    window.removeEventListener('mousemove', handleMouseMove);
    window.removeEventListener('mouseup', handleMouseUp);
    document.removeEventListener('selectstart', preventSelection);
  };
}, [dragState, handleMouseMove, handleMouseUp]);

  return (
    <div className="app" style={{ position: 'relative' }}>
      <h1>Мои Задачи</h1>
      <div className="days-container" style={{ display: 'flex', gap: '20px' }}>
        {days.map(day => (
          <DayColumn
            key={day.id}
            day={day}
            tasks={tasks.filter(t => t.dayId === day.id)}
            onDragStart={handleDragStart}
            dropIndex={
              dropTarget?.dayId === day.id ? dropTarget.index : null
            }
            draggingTaskId={dragState?.task.id || null}
            columnRef={(el) => (columnRefs.current[day.id] = el)}
            taskRefs={taskRefs}
          />
        ))}
      </div>

      {dragState && (
        <DragPreview
          x={previewPos.x}
          y={previewPos.y}
          task={dragState.task}
        />
      )}
    </div>
  );
};

export default App;